using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class Transaction
    {
        public Transaction() { }
        public Guid Id { get; set; }
        public string BuyerId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Finished { get; set; }

    }
}
