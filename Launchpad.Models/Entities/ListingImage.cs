using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class ListingImage
    {
        public ListingImage() { }
        public ListingImage(byte[] image, Listing listing)
        {
            Image = image;
            Listing = listing;
        }
        public Guid Id { get; set; }
        public Boolean IsMainPic { get; set; }
        public byte[] Image { get; set; }

        public Listing Listing { get; set; }
        public Guid ListingId { get; set; }

    }
}
