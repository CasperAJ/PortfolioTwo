using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    public class Search
    {
        public int Id { get; set; }
        public string SearchString { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
