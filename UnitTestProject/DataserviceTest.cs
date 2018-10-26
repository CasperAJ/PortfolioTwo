using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataServiceLayer;
using DataServiceLayer.Models;
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

        [Fact]
        public void GetPost_ValidId_ReturnsCategory()
        {
            var service = new DataService();
            var post = service.GetPostById(19);
            Assert.Equal("What is the fastest way to get the value of π?", post.Title);
        }
    }
}
