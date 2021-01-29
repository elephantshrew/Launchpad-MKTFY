using Launchpad.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class CategoryVM
    {
        public CategoryVM(Category src)
        {
            Name = src.CategoryName;
        }
        [Required]
        public string Name { get; set; }

    }
}
