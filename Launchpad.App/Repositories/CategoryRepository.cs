using Launchpad.App.Repositories.Interfaces;
using Launchpad.Models.Entities;
using Launchpad.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.App.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryVM> Create(CategoryCreateVM src)
        {
            var entity = new Category(src);

            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new CategoryVM(entity);
        }

        public async Task<List<CategoryVM>> GetAll()
        {
            var results = await _context.Categories.ToListAsync();

            var models = new List<CategoryVM>();
            foreach(var entity in results)
            {
                models.Add(new CategoryVM(entity));
            }

            return models;
        }
    }
}
