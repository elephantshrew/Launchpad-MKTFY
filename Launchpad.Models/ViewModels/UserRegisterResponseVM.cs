using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class UserRegisterResponseVM
    {
        [Required]
        public string Response { get; set; }

        public UserRegisterResponseVM(string response)
        {
            Response = response;
        }
    }
}
