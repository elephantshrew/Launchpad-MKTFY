using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class LoginResponseVM
    {
        public LoginResponseVM(TokenResponse tokenresponse, UserVM user)
        {
            AccessToken = tokenresponse.AccessToken;
            Expires = tokenresponse.ExpiresIn;
            User = user;
        }

        [Required]
        public string AccessToken { get; }
        [Required]
        public int Expires { get;  set; }
        [Required]
        public UserVM User { get;  set; }
    }
}
