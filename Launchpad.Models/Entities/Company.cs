using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public String Name { get; set; } 
    }
}
