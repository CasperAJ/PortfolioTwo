using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer.Models;

namespace PortfolioTwo.Models
{
    public class SearchViewModel
    {
        public string Path { get; set; }
        public string Searchstring { get; set; }
        public string UserId { get; set; }
    }
}
