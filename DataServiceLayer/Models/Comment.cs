using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
