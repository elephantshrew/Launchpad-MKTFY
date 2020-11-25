using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class Country
    {
        public Country() { }
        public Country(string name)
        {
            Name = name;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Province> Provinces { get; set; }
    }
}
