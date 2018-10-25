using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    public class TagToPost
    {
        public Tag Tag { get; set; }
        public int TagId { get; set; }

        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
