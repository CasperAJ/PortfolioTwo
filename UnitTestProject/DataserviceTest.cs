using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataServiceLayer;
using Xunit;

namespace UnitTestProject
{
    public class DataserviceTest
    {
        [Fact]
        public void GetAllPosts()
        {
            var service = new DataService();
            var posts = service.GetAllPosts();
            Assert.Equal(13629, posts.Count);
            Assert.Equal(19, posts.First().Id);
            
        }
    }
}
