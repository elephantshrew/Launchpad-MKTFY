using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ResetPasswordVM
    {
        public string Email { get; set; }
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
    }
}
