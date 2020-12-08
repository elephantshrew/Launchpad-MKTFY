using Launchpad.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ListingCreateVM
    {
        //userId, title, description, price, photos
        public string UserId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Images { get; set; }
        //public List<ListingImage> Images { get; set; }
        public String CityName { get; set; }
        public String UserEmail { get; set; }
        public String CategoryName { get; set; }
        public ListingCreateVM() { }

    }
}
