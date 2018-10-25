using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    public abstract class Post
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public int? LinkPostId { get; set; }
    }
}
