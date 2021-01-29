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
                var city1 = new City(new Guid("2696b8f8-9090-4c35-b59c-9b7fa8e3ced8"), "Calgary", province1);
                var city2 = new City(new Guid("ef749f8d-09f0-49b5-8546-8ca10d707143"),"Edmonton", province1);

                var province2 = new Province("British Columbia", country);
                var city3 = new City(new Guid("737ea2b0-f7de-475b-aeae-f73214776c64"), "Vancouver", province2);
                var city4 = new City(new Guid("571f4619-5b12-44ae-97b9-39afda5eb279"), "Kamloops", province2);

                var province3 = new Province("Saskatchewan", country);
                var city5 = new City(new Guid("4568ee0e-1a16-4be9-a6f0-69325e5cecb6"), "Saskatoon", province3);

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
