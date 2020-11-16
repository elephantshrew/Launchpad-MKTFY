using Launchpad.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class CategoryVM
    {
        public string Name { get; set; }
        public CategoryVM(Category src)
        {
            Name = src.CategoryName;
        }
    }
}
