using Microsoft.EntityFrameworkCore;
using Launchpad.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Launchpad.App
{
    //public class UserContext : IdentityDbContext<User>
    //{
    //    public UserContext(DbContextOptions<UserContext> options)
    //    : base(options)
    //    {
    //    }
    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        base.OnModelCreating(builder);
    //    }
    //}

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Company> Companies { get; set; }
    }
}
