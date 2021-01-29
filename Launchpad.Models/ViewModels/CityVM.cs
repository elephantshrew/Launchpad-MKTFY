using Launchpad.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class CityVM
    {
        public CityVM(string name, string provinceName, string countryName)
        {
            Name = name;
            ProvinceName = provinceName;
            CountryName = countryName;
        }
        [Required]
        public string Name {get; set;}
        [Required]
        public string ProvinceName { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}
