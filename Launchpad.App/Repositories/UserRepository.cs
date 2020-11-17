using Launchpad.App;
using Launchpad.App.Repositories.Interfaces;
using Launchpad.Models.Entities;
using Launchpad.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaunchpadSept2020.App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<UserVM> Create(UserRegisterVM src)
        {
            //Create the new entity
            var entity = new User(src);

            //Add and save the changes to the database
            await _context.UserEntities.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new UserVM(entity);

        }

        public async Task<UserVM> GetUserByEmail(string email)
        {
            List<string> a = new List<string>();
            var result = await _context.Users.FirstAsync(item => item.Email == email);

            var model = new UserVM(result);
            return model;
        }

        public async Task<UserVM> GetUserVMById(string id)
        {
            var result = await _context.Users.SingleOrDefaultAsync(b => b.Id == id);
            if (result != null)
            {
                return new UserVM(result);
            }
            else
            {
                return null;
            }
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<UserVM> PatchUser(string id, UserPatchVM vm )
        { 
            var result = await _context.Users.SingleOrDefaultAsync(b => b.Id == id);
            result.PhoneNumber = vm.Phone;
            result.City = vm.City;
            await _context.SaveChangesAsync();
            return new UserVM(result);
        }

    }
}
