using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    public class TagToPosts
    {
        public Tags Tags { get; set; }
        public int TagId { get; set; }

        public Posts Posts { get; set; }
        public int PostId { get; set; }
    }
}
