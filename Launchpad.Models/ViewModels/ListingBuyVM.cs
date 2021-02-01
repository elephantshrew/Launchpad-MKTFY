using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ListingBuyVM
    {
        [Required]
        public Guid ListingId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
