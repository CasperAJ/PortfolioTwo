using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer.Models;

namespace PortfolioTwo.Models
{
    public class CommentViewModel
    {
        public string Path { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Author { get; set; }
        public string Post { get; set; }

    }
}
