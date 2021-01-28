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

        }

        [HttpPost("CreateConnectedAccount")]
        public async Task<ActionResult> Create([FromBody] StripeCreateAccountVM vm)
        {
            StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeTestKey");
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
        public IActionResult CreateCustomer()
        {
            StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeTestKey");

            var options = new CustomerCreateOptions
            {
                Description = "My First Test Customer",
            };
            var service = new CustomerService();
            var customer = service.Create(options);

            return Ok(customer.Id);
        }

        [HttpPost("createpaymentmethod")]
        public IActionResult CreatePaymentMethod([FromBody] StripePaymentMethodVM vm)
        {
            StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeTestKey");

            var options = new PaymentMethodListOptions
            {
                Customer = vm.CustomerId, 
                Type = "card",
            };
            var paymentMethodService = new PaymentMethodService();
            StripeList<PaymentMethod> paymentMethodList = paymentMethodService.List(
              options
            );

            if (paymentMethodList.Count() >= 3)
                return BadRequest("Please remove a payment method first - maximum of 3 payment methods");
            else
            {

                var paymentMethodCreateOptions = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardOptions
                    {
                        Number = vm.CardNumber,
                        ExpMonth = vm.ExpMonth,
                        ExpYear = vm.ExpYear,
                        Cvc = vm.CVC,
                    },
                };
                //var service = new PaymentMethodService();
                var paymentMethod = paymentMethodService.Create(paymentMethodCreateOptions);


                //Attach PaymentMethod to Customer with SetupIntent
                var setupIntentCreateOptions = new SetupIntentCreateOptions
                {
                    PaymentMethod = paymentMethod.Id,
                    Customer = vm.CustomerId
                };
                var setupIntentService = new SetupIntentService();
                var setupIntent = setupIntentService.Create(setupIntentCreateOptions);
                setupIntent = setupIntentService.Confirm(setupIntent.Id);

                return Ok(paymentMethod.Id);
            }
        
        }

        [HttpPost("removepaymentmethod")]
        public IActionResult RemovePaymentMethod()
        {
            //check if Customer has at least >1 card on file. If so, you can remove it
            throw new NotImplementedException("TODO");

        }
    }
}
