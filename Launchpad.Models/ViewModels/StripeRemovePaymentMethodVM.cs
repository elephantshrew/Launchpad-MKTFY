using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class StripeRemovePaymentMethodVM
    {
        public string CustomerId { get; set; }
        public string PaymentMethodId { get; set; }
    }
}
