using Launchpad.App.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.App.Repositories
{

    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}
