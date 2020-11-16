using Launchpad.Models.Entities;
using Launchpad.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.App.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<List<CityVM>> GetAll();
       
    }
}
