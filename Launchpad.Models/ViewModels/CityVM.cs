using Launchpad.Models.Entities;
using System;
using System.Collections.Generic;
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

        public string Name {get; set;}
        public string ProvinceName { get; set; }
        public string CountryName { get; set; }
    }
}
