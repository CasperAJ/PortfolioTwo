using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioTwo.Models
{
    public class AnswerViewModel
    {

        public string path { get; set; }
        public string Parent { get; set; }
        public string Comments { get; set; }

        public DateTime? CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime? ClosedDate { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string LinkPost { get; set; }


    }
}
