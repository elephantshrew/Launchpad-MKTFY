using Launchpad.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ListingCreateVM
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
        [Required]
        public String CityName { get; set; }
        [Required]
        public String UserEmail { get; set; }
        [Required]
        public String CategoryName { get; set; }

        public ListingCreateVM() { }

    }
}
