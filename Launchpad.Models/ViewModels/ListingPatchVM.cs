using Launchpad.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ListingPatchVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        //public List<IFormFile> Photos { get; set; }
        public List<ListingImage> Photos { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }


    }
}
