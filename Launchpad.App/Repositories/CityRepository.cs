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
                var province = await _context.Provinces.SingleAsync(b => b.Id == result.ProvinceId);
                var country = await _context.Countries.SingleAsync(b => b.Id == province.CountryId);
                models.Add(new CityVM(result.Name, province.Name, country.Name));
            }

            return models;
        }
    }
}
