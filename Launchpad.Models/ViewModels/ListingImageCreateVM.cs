using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ListingImageCreateVM
    {
        [Required]
        public Boolean IsMainPic { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
