using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioTwo.Models
{
    public class QuestionViewModel
    {
        public string path { get; set; }
        public string Answers { get; set; }

        public string AcceptedAnswer { get; set; }

        public DateTime? CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime? ClosedDate { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string LinkPost { get; set; }
    }
}
