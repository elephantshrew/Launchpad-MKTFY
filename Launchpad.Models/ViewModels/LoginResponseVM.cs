using IdentityModel.Client;
using System;
using System.Collections.Generic;
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

        public string AccessToken { get; }
        public int Expires { get;  set; }
        public UserVM User { get;  set; }
    }
}
