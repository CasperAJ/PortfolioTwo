using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataServiceLayer.Models;

namespace DataServiceLayer
{
    public class DataService
    {
        private StackOverflowContext db;

        public DataService()
        {
            db = new StackOverflowContext();
        }

        //Posts 
        public List<Post> GetAllPosts()
        {
            return db.Posts.OrderBy(x=> x.Id).ToList();
        }
    }
}
