using System;
using System.Collections.Generic;
using System.Text;

namespace Launchpad.Models.Entities
{
    public class FAQ
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public FAQ() { }
    }
}
