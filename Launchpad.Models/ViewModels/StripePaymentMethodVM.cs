using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{

    public class StripePaymentMethodVM
    {
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
