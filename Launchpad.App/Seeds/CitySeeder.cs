using Launchpad.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.App.Seeds
{
    public class CitySeeder
    {
        private ApplicationDbContext _context;

        public CitySeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            //await _context.Database.ExecuteSqlRawAsync("DELETE FROM \"Cities\"");
            //await _context.Database.ExecuteSqlRawAsync("DELETE FROM \"Provinces\"");
            //await _context.Database.ExecuteSqlRawAsync("DELETE FROM \"Countries\"");
            var testEntity = await _context.Cities.FirstOrDefaultAsync();
            if (testEntity == null)
            {
                var country = new Country("Canada");
                var province1 = new Province("Alberta", country);
                var city1 = new City("Calgary", province1);
                var city2 = new City("Edmonton", province1);

                var province2 = new Province("British Columbia", country);
                var city3 = new City("Vancouver", province2);
                var city4 = new City("Kamloops", province2);

                var province3 = new Province("Saskatchewan", country);
                var city5 = new City("Saskatoon", province3);

                _context.Countries.Add(country);
                _context.Provinces.Add(province1);
                _context.Provinces.Add(province2);
                _context.Provinces.Add(province3);
                _context.Cities.Add(city1);
                _context.Cities.Add(city2);
                _context.Cities.Add(city3);
                _context.Cities.Add(city4);
                _context.Cities.Add(city5);
            }
            await _context.SaveChangesAsync();
        }
    }
}
