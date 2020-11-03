using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class UserRegisterResponseVM
    {
        public string Response { get; set; }

        public UserRegisterResponseVM(string response)
        {
            Response = response;
        }
    }
}
