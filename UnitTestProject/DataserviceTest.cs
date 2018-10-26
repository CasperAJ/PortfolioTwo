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


















        // Get all searches
        [Fact]
        public void GetAllSearches()
        {
            var service = new DataService();
            var searches = service.GetAllSearches();
            Assert.Equal(9, searches.Count);
            Assert.Equal(4, searches.Last().UserId);
        }

        // Get all searches for user
        [Fact]
        public void GetAllSearchesForUser_ValidReturn()
        {
            var service = new DataService();
            var searches = service.GetAllSearchesByUserId(2);
            Assert.Equal(3,searches.Count);
            Assert.Equal(2, searches.First().Id);
            Assert.Equal("sql params csharp", searches.First().SearchString);
            Assert.Equal(2, searches.First().UserId);
        }


        // Get author by Id
        [Fact]
        public void GetAuthor_VallidReturn()
        {
            var service = new DataService();
            var author = service.GetAuthor(1);
            Assert.Equal("Jeff Atwood", author.Name);
            Assert.Equal(new DateTime(2008,07,31,14,22,31), author.CreationDate);
            Assert.Equal("El Cerrito, CA", author.Location);
            Assert.Equal(45, author.Age);
        }

    }
}
