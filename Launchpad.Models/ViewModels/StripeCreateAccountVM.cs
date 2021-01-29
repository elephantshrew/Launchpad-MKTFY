using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class StripeCreateAccountVM
    {
        [Required]
        public string UserId { get; set; }
    }
}
