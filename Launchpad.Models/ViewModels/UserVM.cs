using Launchpad.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class UserVM
    {
        public UserVM(User src)
        {
            Id = src.Id;
            Email = src.Email;
            FirstName = src.FirstName;
            LastName = src.LastName;
            Phone = src.PhoneNumber;
            City = src.CityName;
        }

        public String Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        
    }
}

