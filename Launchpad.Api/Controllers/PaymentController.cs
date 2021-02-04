using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Launchpad.App;
using Launchpad.Models.Entities;
using Launchpad.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;


namespace Launchpad.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public PaymentController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
            StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeTestKey");
        }

        [HttpPost("CreateConnectedAccount")]
        public async Task<ActionResult> Create([FromBody] StripeCreateAccountVM vm)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == vm.UserId);

                var options = new AccountCreateOptions
                {
                    Type = "express",
                    Email = user.Email
                };

                var service = new AccountService();
                var account = await service.CreateAsync(options);

                var hostName = _configuration.GetValue<string>("Hostname");
                var linkOptions = new AccountLinkCreateOptions
                {
                    Account = account.Id,
                    RefreshUrl = hostName + "/api/reauth/" + vm.UserId, //see accountcontroller
                    ReturnUrl = hostName + "/api/return/" + vm.UserId,
                    Type = "account_onboarding",
                };
                var accountLinkService = new AccountLinkService();
                var accountLink = await accountLinkService.CreateAsync(linkOptions);

                return Ok(accountLink.Url);
            }
            catch (StripeException e)
            {
                return StatusCode(500, (new { error = e.StripeError.Message }));
            }
        }

        [HttpGet("return/{id}")]
        public async Task<IActionResult> AccountCreationReturn([FromRoute] string id)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(b => b.Id == id);
                if (user == null)
                    return BadRequest();
                var service = new AccountService();
                var account = await service.GetAsync(user.StripeConnectedAccountId);
                if (account.ChargesEnabled == false || account.PayoutsEnabled == false)
                {
                    return Ok("User must complete onboarding later");
                }
                return Ok("Success");
            }

            
            catch (StripeException e)
            {
                return StatusCode(500, (new { error = e.StripeError.Message }));
            }
        }

        [HttpGet("reauth/{id}")]
        public async Task<IActionResult> AccountCreationRefresh([FromRoute] string id)
        {
            return await Create(new StripeCreateAccountVM { UserId = id });
        }

        [HttpPost("createcustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerCreateVM vm)
        { 
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == vm.UserId);

            try
            {
                var options = new CustomerCreateOptions
                {
                    Email = user.Email,
                };
                var service = new CustomerService();
                var customer = await service.CreateAsync(options);
                var customerForContext = new Models.Entities.Customer { Id = customer.Id, UserId = vm.UserId };
                var result = await _context.Customers.AddAsync(customerForContext);
                await _context.SaveChangesAsync();

                return Ok(customer.Id);
            }
            catch (StripeException e)
            {
                return StatusCode(500, (new { error = e.StripeError.Message }));
            }
        }

        [HttpPost("addpaymentmethod")]
        public async Task<IActionResult> CreatePaymentMethod([FromBody] StripeAddPaymentMethodVM vm)
        {
            try
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(b => b.UserId == vm.UserId); //TODO: check if customer null
                if (customer == null)
                    return BadRequest("Not found - buyer id");
                var paymentMethodList = await _context.Payments.Where(b => b.CustomerId == customer.Id).ToListAsync();

                if (paymentMethodList.Count() >= 3)
                    return BadRequest("Please remove a payment method first - maximum of 3 payment methods");
                else
                {
                    //Attach PaymentMethod to Customer with SetupIntent
                    var setupIntentCreateOptions = new SetupIntentCreateOptions
                    {
                        PaymentMethod = vm.PaymentMethodId,
                        Customer = customer.Id
                    };
                    var setupIntentService = new SetupIntentService();
                    var setupIntent = await setupIntentService.CreateAsync(setupIntentCreateOptions);
                    setupIntent = await setupIntentService.ConfirmAsync(setupIntent.Id);
                    if (setupIntent.Status == "succeeded")
                    {
                        if(vm.SetAsDefault == true || paymentMethodList.Count == 0)
                        {
                            var options = new CustomerUpdateOptions
                            {
                                InvoiceSettings = new CustomerInvoiceSettingsOptions
                                {
                                    DefaultPaymentMethod = setupIntent.PaymentMethodId
                                }
                            };
                            var customerService = new CustomerService();
                            var result = await customerService.UpdateAsync(customer.Id, options);
                        }

                        var payment = new Payment {CustomerId = customer.Id, Id = setupIntent.PaymentMethodId};
                        await _context.Payments.AddAsync(payment);
                        await _context.SaveChangesAsync();
                        return Ok(setupIntent.Status);
                        
                    }
                    else
                        return StatusCode(500, setupIntent.Status);
                }
            }
            catch (StripeException e)
            {
                return StatusCode(500, (new { error = e.StripeError.Message }));
            }
        }

        [HttpPost("removepaymentmethod")]
        public async Task<IActionResult> RemovePaymentMethod([FromBody] StripeRemovePaymentMethodVM vm)
        {
            //check if Customer has at least >1 card on file. If so, you can remove it
            try
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(b => b.UserId == vm.UserId);
                var paymentMethodList = await _context.Payments.Where(b => b.CustomerId == customer.Id).ToListAsync();

                if (paymentMethodList.Count() <= 1)
                    return BadRequest("Must have at least 1 payment method on file");
                else
                {
                    var paymentMethodService = new PaymentMethodService();
                    await paymentMethodService.DetachAsync(vm.PaymentMethodId);
                    var paymentMethod = await _context.Payments.SingleOrDefaultAsync(b => b.Id == vm.PaymentMethodId);
                    _context.Payments.Remove(paymentMethod);
                    var result = await _context.SaveChangesAsync();
                    return Ok("Payment method " + vm.PaymentMethodId + " removed");
                }
            }
            catch (StripeException e)
            {
                return StatusCode(500, (new { error = e.StripeError.Message }));
            }

        }

        /*
         * On payment, price + $2.50 service charge is taken out of the buyer's account and price is credited to seller's account. $2.50 is credited to MKTFY account.
         * */

            [HttpPost("buy")]
        public async Task<IActionResult> BuyListing(ListingBuyVM vm)
        {
            //Get listing
            var listing = await _context.Listings.SingleOrDefaultAsync(b => b.Id == vm.ListingId);
            if (listing == null)
                return BadRequest("Listing not found");
            var buyer = await _context.Users.SingleOrDefaultAsync(b => b.Id == vm.UserId);
            if (buyer == null)
                return BadRequest("User not found");

            var seller = await _context.Users.SingleOrDefaultAsync(b => b.Id == listing.UserId);

            var connectedStripeAccountId = seller.StripeConnectedAccountId;
            var amount = listing.Price;
            var buyerCustomer = await _context.Customers.SingleOrDefaultAsync(b => b.UserId == buyer.Id);
            if (buyerCustomer == null)
                return BadRequest("Customer not found");

            var service = new CustomerService();
            var buyerCustomerFromService = await service.GetAsync(buyerCustomer.Id);
            var paymentMethodId = buyerCustomerFromService.InvoiceSettings.DefaultPaymentMethodId;
            if (paymentMethodId == null)
            {
               return BadRequest("Payment method not found");
            }
           
            try
            {
                var paymentIntentService = new PaymentIntentService();
                var createOptions = new PaymentIntentCreateOptions
                {
                    Customer = buyerCustomer.Id,
                    PaymentMethod = paymentMethodId,
                    PaymentMethodTypes = new List<string>
                {
                "card",
                },
                    Amount = (long)amount + 250,
                    Currency = "cad",
                    ApplicationFeeAmount = 250,
                    TransferData = new PaymentIntentTransferDataOptions
                    {
                        Destination = connectedStripeAccountId
                    },
                };

                var paymentIntent = await paymentIntentService.CreateAsync(createOptions);
                paymentIntent = await paymentIntentService.ConfirmAsync(paymentIntent.Id);
                var transaction = new Transaction { Id = vm.ListingId, BuyerId = vm.UserId, Created = DateTime.Now };
                if(paymentIntent.Status == "succeeded")
                {
                    transaction.Finished = DateTime.Now;
                }
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return GeneratePaymentResponse(paymentIntent);
            }
            catch (StripeException e)
            {
                return StatusCode(500, (new { error = e.StripeError.Message }));
            }
        }

        private async Task<IActionResult> TransferPayment(StripeTransferPaymentVM vm)
        {
            try
            {
                var paymentIntentService = new PaymentIntentService();
                var createOptions = new PaymentIntentCreateOptions
                {
                    Customer = vm.CustomerId,
                    PaymentMethod = vm.PaymentMethodId,
                    PaymentMethodTypes = new List<string>
                {
                "card",
                },
                    Amount = vm.Amount + 250,
                    Currency = "cad",
                    ApplicationFeeAmount = 250,
                    TransferData = new PaymentIntentTransferDataOptions
                    {
                        Destination = vm.ConnectedStripeAccountId
                    },
                };

                var paymentIntent = await paymentIntentService.CreateAsync(createOptions);
                paymentIntent = await paymentIntentService.ConfirmAsync(paymentIntent.Id);
                return GeneratePaymentResponse(paymentIntent);
            }
            catch (StripeException e)
            {
                return StatusCode(500, (new { error = e.StripeError.Message }));
            }
        }



        private IActionResult GeneratePaymentResponse(PaymentIntent intent)
        {
            if (intent.Status == "succeeded")
            {
                // Handle post-payment fulfillment
                return Ok("Succeeded");
            }
            else
            {
                // Any other status would be unexpected, so error
                return StatusCode(500, new { error = "Invalid PaymentIntent status" });
            }
        }

    }
}
