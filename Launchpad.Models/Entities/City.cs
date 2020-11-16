using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class City
    {
        public string Name { get; set; } 
        public Guid Id { get; set; }
        public Province Province { get; set; }
    }
}
