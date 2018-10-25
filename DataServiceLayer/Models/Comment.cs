using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLayer.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public Authors Author { get; set; }
        public int AuthorsId { get; set; }
        public Posts Post { get; set; }
        public int PostsId { get; set; }
    }
}
