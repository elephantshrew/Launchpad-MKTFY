using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class Province
    {
        public Province() { }

        public Province(string name, Country country) 
        {
            Name = name;
            Country = country;
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
