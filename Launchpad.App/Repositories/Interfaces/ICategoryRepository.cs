using Launchpad.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad.App.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryVM> Create(CategoryCreateVM src);

        Task<List<CategoryVM>> GetAll();
    }
}
