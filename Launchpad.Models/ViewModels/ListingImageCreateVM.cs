using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.ViewModels
{
    public class ListingImageCreateVM
    {
        public Boolean IsMainPic { get; set; }
        public IFormFile Image { get; set; }
    }
}
