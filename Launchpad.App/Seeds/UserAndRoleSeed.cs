using Launchpad.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.App.Seeds
{
    public static class UserAndRoleSeeder
    {
        public static async Task Seed(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            var roleResult = await roleManager.RoleExistsAsync("administrator");
            if(!roleResult)
            {
                await roleManager.CreateAsync(new IdentityRole("administrator"));
            }

            roleResult = await roleManager.RoleExistsAsync("user");
            if (!roleResult)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            var userResult = await userManager.FindByNameAsync("william@launchpadbyvog.com");
            if (userResult == null)
            {
                var user = new User
                {
                    UserName = "william@launchpadbyvog.com",
                    Email = "william@launchpadbyvog.com",
                    FirstName = "william",
                    LastName = "Admin",
                    CityId = new Guid("2696b8f8-9090-4c35-b59c-9b7fa8e3ced8"),
                    CityName = "Calgary"
                };

                IdentityResult result = await userManager.CreateAsync(user, "Password1");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "administrator");
                }

            }

        }
    }
}
