using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string ResetToken { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
