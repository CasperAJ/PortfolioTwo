using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
    }
}
