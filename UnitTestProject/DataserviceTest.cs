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
        //Posts
        [Fact]
        public void GetAllPosts_NoArguments()
        {
            var service = new DataService();
            var posts = service.GetAllPosts();
            Assert.Equal(13629, posts.Count);
            Assert.Equal(19, posts.First().Id);
            
        }

        [Fact]
        public void GetPost_ValidId_ReturnsPost()
        {
            var service = new DataService();
            var post = service.GetPostById(19);
            Assert.Equal("What is the fastest way to get the value of π?", post.Title);
        }

        //Comments
        [Fact]
        public void GetAllComments_NoArguments()
        {
            var service = new DataService();
            var comment = service.GetAllComments();
            Assert.Equal(120, comment.First().Id);
            Assert.Equal(32042, comment.Count);
        }

        [Fact]
        public void GetComment_ValidId_ReturnsComment()
        {
            var service = new DataService();
            var comment = service.GetCommentById(674);
            Assert.Equal("Your co-workers are going to hate you.", comment.Text);
        }
    }
}
