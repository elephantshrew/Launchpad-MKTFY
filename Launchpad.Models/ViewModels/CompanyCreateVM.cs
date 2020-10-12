using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class CompanyCreateVM
    {
        [Required]
        public String Name { get; set; }
    }
}
