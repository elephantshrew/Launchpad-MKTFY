using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
