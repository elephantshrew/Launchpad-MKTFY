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
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> UserEntities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingImage> ListingImages { get; set; }
        public DbSet<FAQ> FAQs { get; set; }

       
    }
}
