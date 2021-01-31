using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class Payment
    {
        public Payment() { }
        public string Id { get; set; }
        public string StripePaymentMethodId { get; set; }
        public string CustomerId { get; set; }
        
        public Customer Customer { get; set; }

    }
}
