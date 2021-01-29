using Launchpad.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ListingPatchVM
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public List<ListingImage> Photos { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public string UserId { get; set; }


    }
}
