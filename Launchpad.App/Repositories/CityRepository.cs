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
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CityVM>> GetAll()
        {
            var results = await _context.Cities.ToListAsync();

            var models = new List<CityVM>();

            foreach(var result in results)
            {
                models.Add(new CityVM(result));
            }

            return models;
        }
    }
}
