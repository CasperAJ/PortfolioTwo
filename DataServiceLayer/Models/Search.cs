using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    class Searches
    {
        public int Id { get; set; }
        public string SearchString { get; set; }

        public Users Users { get; set; }
        public int UserId { get; set; }
    }
}
