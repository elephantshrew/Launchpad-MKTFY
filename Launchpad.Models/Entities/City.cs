using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class City
    {
        public City()
        {
        }


        public City(Guid id, string name, Province province)
        {
            Id = id;
            Name = name;
            Province = province;
        }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Province Province { get; set; }
        public Guid ProvinceId { get; set; }
        public ICollection<User> Users {get; set;}
        public ICollection<Listing> Listings {get; set;}
    }
}
