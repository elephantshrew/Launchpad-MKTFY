using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{

    public class StripeCreatePaymentMethodVM    
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public int ExpMonth { get; set; }

        [Required]
        public int ExpYear { get; set; }

        [Required]
        public string CVC { get; set; }

    }
}
