using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int AcceptedAnswerId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime CloseDate { get; set; }
        public string Title { get; set; }
        public Authors Author { get; set; }
        public int AuthorsId { get; set; }
        public int LinkPostId { get; set; }
    }
}
