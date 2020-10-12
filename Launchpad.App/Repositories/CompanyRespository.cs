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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<CompanyVM> Create(CompanyCreateVM src)
        {
            //Create the new entity
            var entity = new Company(src);

            //Add and save the changes to the database
            await _context.Companies.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new CompanyVM(entity);

        }

        public async Task<List<CompanyVM>> GetAll()
        {
            //get all the entities
            var results = await _context.Companies.ToListAsync();

            //build view models
            var models = new List<CompanyVM>();
            foreach (var entity in results)
                models.Add(new CompanyVM(entity));

            return models;              
        }
    }
}
