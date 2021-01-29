using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class UserRegisterVM
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        public String PasswordConfirmation { get; set; }
        [Required]
        public DateTime Tos { get; set; }
        [Required]
        public String Phone { get; set; }
        [Required]
        public String City { get; set; }
    }
}
