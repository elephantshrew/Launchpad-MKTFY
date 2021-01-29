using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ListingResponseVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public List<byte[]> Images { get; set; }
        [Required]
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
