using Launchpad.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.App.Seeds
{
    public class FAQSeeder
    {
        private ApplicationDbContext _context;

        public FAQSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {

            var testEntity = await _context.FAQs.FirstOrDefaultAsync();
            if (testEntity == null)
            {
                var entity1 = new FAQ {Title = "Question 1", Text = "Answer 1", Created = DateTime.Now, Updated = DateTime.Now };
                var entity2 = new FAQ { Title = "Question 2", Text = "Answer 2", Created = DateTime.Now, Updated = DateTime.Now };
                var entity3 = new FAQ { Title = "Question 3", Text = "Answer 3", Created = DateTime.Now, Updated = DateTime.Now };
                var entity4 = new FAQ { Title = "Question 4", Text = "Answer 4", Created = DateTime.Now, Updated = DateTime.Now };

                _context.FAQs.Add(entity1);
                _context.FAQs.Add(entity2);
                _context.FAQs.Add(entity3);
                _context.FAQs.Add(entity4);
            }
            await _context.SaveChangesAsync();
        }
    }
}
