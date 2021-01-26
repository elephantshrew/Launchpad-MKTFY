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
            var account = service.Create(options);

            var hostName = _configuration.GetValue<string>("Hostname");
            var linkOptions = new AccountLinkCreateOptions
            {
                Account = account.Id,       
                RefreshUrl = hostName + "/api/reauth", 
                ReturnUrl = hostName + "/api/return",
                Type = "account_onboarding",
            };
            var accountLinkService = new AccountLinkService();
            var accountLink = accountLinkService.Create(linkOptions);

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


    }
}
