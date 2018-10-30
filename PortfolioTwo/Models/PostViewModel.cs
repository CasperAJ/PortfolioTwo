using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioTwo.Models
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public int? ParentId { get; set; }
        public int? AcceptedAnswerId { get; set; }

        public DateTime? CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime? ClosedDate { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public int? LinkPostId { get; set; }
    }
}
