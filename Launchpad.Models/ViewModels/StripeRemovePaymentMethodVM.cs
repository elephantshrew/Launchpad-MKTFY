using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class StripeRemovePaymentMethodVM
    {
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string PaymentMethodId { get; set; }
    }
}
