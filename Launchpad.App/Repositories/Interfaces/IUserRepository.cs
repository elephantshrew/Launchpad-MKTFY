﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Launchpad.Models;
using Launchpad.Models.ViewModels;

namespace Launchpad.App.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserVM> GetUserByEmail(string email);
        Task<UserVM> GetUserById(string id);
        //Task<UserVM> Create(UserRegisterVM user);
    }
}
