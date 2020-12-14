using Launchpad.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Launchpad.Models.Entities
{
    public class Category
    {
        public Category()
        {

        }
        public Category(CategoryCreateVM src)
        {
            CategoryName = src.Name;
        }

        public Category(string name)
        {
            CategoryName = name;
        }
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        [JsonIgnore]
        public ICollection<Listing> Listings {get; set;}
    }
}
