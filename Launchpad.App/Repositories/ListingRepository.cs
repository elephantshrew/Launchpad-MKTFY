using Launchpad.App.Repositories.Interfaces;
using Launchpad.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.App.Repositories
{
    public class ListingRepository : IListingRepository
    {
        public Task<Guid> CreateListing(ListingCreateVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
