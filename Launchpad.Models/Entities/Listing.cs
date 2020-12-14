using Launchpad.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class Listing
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
     
        //public Guid UserID { get; set; }
        public User User { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public ICollection<ListingImage> ListingImages { get; set; }
        public string UserId { get; set; }
        public Category Category { get; set; }
        public Boolean DisplayInSearches { get; set; } = true;


        public Listing() { }
        public Listing(ListingCreateVM vm, City city, User user, Category category)
        {
            //UserId = vm.UserId;
            Title = vm.Title;
            Description = vm.Description;
            Price = vm.Price;
            Description = vm.Description;
            City = city;
            User = user;
            Category = category;
        }

        public Listing(ListingCreateVM vm, City city, User user, ICollection<ListingImage> listingImages)
        {
            //UserId = vm.UserId;
            Title = vm.Title;
            Description = vm.Description;
            Price = vm.Price;
            Description = vm.Description;
            City = city;
            User = user;
            ListingImages = listingImages;
        }


    }
}
