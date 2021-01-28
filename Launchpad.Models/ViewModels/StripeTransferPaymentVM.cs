using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class StripeTransferPaymentVM
    {
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string PaymentMethodId { get; set; }
        [Required]
        public long Amount { get; set; }
        [Required]
        public string ConnectedStripeAccountId { get; set; }
    }
}
