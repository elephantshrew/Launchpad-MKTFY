using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ListingResponseVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<byte[]> Images { get; set; }
        public string Owner { get; set; }

        public ListingResponseVM() { }

        public ListingResponseVM(string title, string description, decimal price, List<byte[]> images, string owner ) 
        {
            Title = title;
            Description = description;
            Price = price;
            Images = images;
            Owner = owner;
        }

    }
}
