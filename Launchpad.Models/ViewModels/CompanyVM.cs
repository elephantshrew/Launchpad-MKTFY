using Launchpad.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels 
{ 

    public class CompanyVM
    {
        public CompanyVM() { }
        public CompanyVM(Company src)
        {
            Id = src.Id;
            Name = src.Name;

        }
        [Required]
        public Guid Id { get; set; }
        [Required]
        public String Name { get; set; }
    }
}
