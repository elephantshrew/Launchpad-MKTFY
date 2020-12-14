using Launchpad.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

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

        public ListingImage(ListingImageCreateVM vm, Listing listing)
        {
            //using (var memoryStream = new MemoryStream())
            //{
            //    vm.Image.CopyToAsync(memoryStream);
            //    Image = memoryStream.ToArray();            
            //}
            ConvertToBytes(vm);
            Listing = listing;
            ListingId = listing.Id;
        }
        public async void ConvertToBytes(ListingImageCreateVM vm)
        {
            using (var memoryStream = new MemoryStream())
            {
                await vm.Image.CopyToAsync(memoryStream);
                Image = memoryStream.ToArray();
            }

        }

        public Guid Id { get; set; }
        public Boolean IsMainPic { get; set; }
        public byte[] Image { get; set; }

        [JsonIgnore]
        public Listing Listing { get; set; }
        public Guid ListingId { get; set; }

    }
}
