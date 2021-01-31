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
            //StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeTestKey");
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
                    RefreshUrl = hostName + "/api/reauth",
                    ReturnUrl = hostName + "/api/return",
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

        [HttpGet("return")]
        public IActionResult AccountCreationReturn()
        {
            return Ok();
        }

        [HttpGet("reauth")]
        public IActionResult AccountCreationRefresh()
        {
            return Ok();
        }

        [HttpPost("createcustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerCreateVM vm)
        {
            //StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeTestKey");

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == vm.UserId);

            try
            {
                var options = new CustomerCreateOptions
                {
                    Email = user.Email,
                };
                var service = new CustomerService();
                var customer = service.Create(options);
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
            //StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeTestKey");
            try
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(b => b.UserId == vm.UserId);
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
                    var setupIntent = setupIntentService.Create(setupIntentCreateOptions);
                    setupIntent = setupIntentService.Confirm(setupIntent.Id);
                    if (setupIntent.Status == "succeeded")
                    {
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
            //StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeTestKey");

            try
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(b => b.UserId == vm.UserId);
                var paymentMethodList = await _context.Payments.Where(b => b.CustomerId == customer.Id).ToListAsync();

                if (paymentMethodList.Count() <= 1)
                    return BadRequest("Must have at least 1 payment method on file");
                else
                {
                    var paymentMethodService = new PaymentMethodService();
                    paymentMethodService.Detach(vm.PaymentMethodId);
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
        public IActionResult TransferPayment(StripeTransferPaymentVM vm)
        {
            //StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeTestKey");
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

                var paymentIntent = paymentIntentService.Create(createOptions);
                paymentIntent = paymentIntentService.Confirm(paymentIntent.Id);
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
