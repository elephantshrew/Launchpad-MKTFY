using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Launchpad.Models;
using Launchpad.Models.Entities;
using Launchpad.Models.ViewModels;

namespace Launchpad.App.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserVM> GetUserByEmail(string email);
        Task<UserVM> GetUserVMById(string id);
        Task<User> GetUserById(string id);
        Task<UserVM> PatchUser(string id, UserPatchVM vm);
        //Task<UserVM> Create(UserRegisterVM user);
    }
}
