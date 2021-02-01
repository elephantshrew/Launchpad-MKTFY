using Launchpad.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.App.Utilities
{
    public class StripeUtility
    {
        private readonly IConfiguration _configuration;
        public StripeUtility(IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = _configuration.GetValue<string>("StripeTestKey");
            _configuration = configuration;
        }
        public void CreateConnectedAccount(StripeCreateAccountVM vm)
        {
            
        }
        public void CreatePaymentMethod(StripeAddPaymentMethodVM vm)
        {
            throw new NotImplementedException("TODO");
        }

        public void CreateCustomer(StripeAddPaymentMethodVM vm)
        {
            throw new NotImplementedException("TODO");
        }
    }
}
