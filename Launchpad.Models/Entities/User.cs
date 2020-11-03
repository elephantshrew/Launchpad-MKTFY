﻿using Launchpad.Models.ViewModels;
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
        [Required]
        public string Email { get; set; }

        public User() { }

        
        public User(UserRegisterVM src)
        {
            FirstName = src.FirstName;
            LastName = src.LastName;
            Email = src.Email;
        }
    }
}
