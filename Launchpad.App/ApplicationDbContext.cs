using Microsoft.EntityFrameworkCore;
using Launchpad.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.App
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
    }
}
