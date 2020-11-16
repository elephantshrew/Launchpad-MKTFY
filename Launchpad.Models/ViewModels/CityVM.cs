using Launchpad.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class CityVM
    {
        public CityVM(City city)
        {
            Name = city.Name;
        }

        public string Name {get; set;}
    }
}
