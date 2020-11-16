using Launchpad.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.App.Seeds
{

    public class CategorySeeder
    {
        private ApplicationDbContext _context;

        public CategorySeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {

            var testEntity = await _context.Categories.FirstOrDefaultAsync();
            if (testEntity == null)
            {
                var entity1 = new Category("Electronics");
                var entity2 = new Category("Household");
                var entity3 = new Category("Food");
                var entity4 = new Category("Clothing");

                _context.Categories.Add(entity1);
                _context.Categories.Add(entity2);
                _context.Categories.Add(entity3);
                _context.Categories.Add(entity4);
            }
            await _context.SaveChangesAsync();
        }

    }
}
