using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioTwo.Models
{
    public class MarkingViewModel
    {
        public string Path { get; set; }
        public int Type { get; set; }
        public string Post { get; set; }
        
        // at this point there is no "point", in setting the user, because we only look at markings based on the user.
        //public string User { get; set; }
        public string Note { get; set; }

    }
}
