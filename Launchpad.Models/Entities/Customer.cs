using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class Customer
    { 
        public Customer() { }

        public string Id { get; set; }
        //public string PaymentId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
