using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public MarkType MarkType { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Note { get; set; }

    }
}
