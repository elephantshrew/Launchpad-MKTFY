using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{

    public class StripeAddPaymentMethodVM    
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string PaymentMethodId { get; set; }
        [Required]
        public bool SetAsDefault { get; set; }

    }
}
