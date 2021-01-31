using Launchpad.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime Tos { get; set; }
        public DateTime LastLogin { get; set; }
        public string CityName { get; set; }

        public Guid CityId { get; set; }
        public string StripeConnectedAccountId { get; set; }
        public City City { get; set; }
        public Customer Customer { get; set; }

        public User() { }
        public User(UserRegisterVM src)
        {
            FirstName = src.FirstName;
            LastName = src.LastName;
            Email = src.Email;
        }
    }
}
