using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioTwo.Models
{
    public class AuthorViewModel
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
    }
}
