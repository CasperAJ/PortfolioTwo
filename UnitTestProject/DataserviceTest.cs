﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataServiceLayer;
using DataServiceLayer.Models;
using PortfolioTwo.Services;
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
            Assert.Equal(10, posts.Count);
            Assert.Equal(19, posts.First().Id);

            //Test prior to paging
            //Assert.Equal(13629, posts.Count);
        }

        [Fact]
        public void GetPost_ValidId_ReturnsPost()
        {
            var service = new DataService();
            var post = service.GetPostById(19);
            Assert.Equal("What is the fastest way to get the value of π?", post.Title);
        }

        [Fact]
        public void GetPost_InvalidId_ReturnNull()
        {
            var service = new DataService();
            var post = service.GetPostById(-1);
            Assert.Null(post);
        }

        //Comments
        [Fact]
        public void GetAllComments_NoArguments()
        {
            var service = new DataService();
            var comment = service.GetAllComments();
            Assert.Equal(120, comment.First().Id);
            Assert.Equal(10, comment.Count);

            //Test prior to paging
            //Assert.Equal(32042, comment.Count);
        }

        [Fact]
        public void GetComment_ValidId_ReturnsComment()
        {
            var service = new DataService();
            var comment = service.GetCommentById(674);
            Assert.Equal("Your co-workers are going to hate you.", comment.Text);
        }

        [Fact]
        public void GetComments_ByPostId_ReturnsComments()
        {
            var service = new DataService();
            var comments = service.GetCommentsByPostId(39433);
            Assert.Equal(10, comments.Count);
            Assert.Equal(70616, comments.First().AuthorId);
            Assert.Equal(4052, comments.Last().AuthorId);

            //Test prior to paging
            //Assert.Equal(27, comments.Count
        }

        //Users
        [Fact]
        public void GetUser_ByValidId()
        {
            var service = new DataService();
            var user = service.GetUser(1);
            Assert.Equal("user01", user.UserName);
        }

        [Fact]
        public void CreateUser_ByValidId()
        {
            var service = new DataService();

            var username = "NewTestUser1";
            var password = "test";
            var email = "NewTestUser1@h.com";
            var size = 256;
            var salt = PasswordService.GenerateSalt(size);
            var pwd = PasswordService.HashPassword(password, salt, size);
            
            var user = service.CreateUser(username, pwd, salt, email);
            Assert.True(user.Id > 0);
            Assert.Equal("NewTestUser1", user.UserName);
        }

        [Fact]
        public void UpdateUser_WithEmailAndPassword()
        {
            var service = new DataService();
            var username = "NewTestUser2";
            var password = "test";
            var email = "NewTestUser2@h.com";
            var size = 256;
            var salt = PasswordService.GenerateSalt(size);
            var pwd = PasswordService.HashPassword(password, salt, size);

            var user = service.CreateUser(username, pwd, salt, email);

            var result = service.UpdateUser(user.Id, "newUserTest@email.dk", "newPassword500");
            Assert.True(result);

            Assert.Equal("newPassword500", user.Password);
            Assert.Equal("newUserTest@email.dk", user.Email);
        }

        // Get all marks for user
        [Fact]
        public void GetAllMarksByUser_ValidReturn()
        {
            var service = new DataService();
            var mark = service.GetAllMarksByUser(1);
            Assert.Equal(5, mark.Count);
            Assert.Equal(24205154, mark.Last().PostId);
        }

        // Get User Mark By MarkType
        [Fact]
        public void GetUserMarkByMarkType_ValidReturn()
        {
            var service = new DataService();
            var mark1 = service.GetUserMarkByMarkType(1, 1);
            Assert.Equal(3,mark1.Count);
            Assert.Equal(22548689, mark1.Last().PostId);

            var mark2 = service.GetUserMarkByMarkType(1, 2);
            Assert.Equal(2, mark2.Count);
            Assert.Equal(24205154, mark2.Last().PostId);
        }

        // Get all marks for user
        [Fact]
        public void GetMarkByIdForUser_ValidReturn()
        {
            var service = new DataService();
            var mark = service.GetMarkByIdForUser(20350933, 1, 1);
            Assert.Equal(1, mark.Type);
            Assert.Equal(1, mark.UserId);
        }

        // Create new Mark
        [Fact]
        public void CreateMark_validReturn()
        {
            var service = new DataService();
            var newMark1 = service.CreateMark(983366, 1, 1, "This is a test note");
            Assert.True(newMark1);

            //var newMark2 = service.CreateMark(65628, 2, 2, "");
            //Assert.True(newMark2);
        }

        // Delete Mark
        [Fact]
        public void DeleteMark_ValidReturn()
        {
            var service = new DataService();
            var mark = service.DeleteMark(983366, 1, 1);
            Assert.True(mark);
        }

        //Get search by search string
       [Fact]
        public void GetSearchBySearchString_ValidReturn()
        {
            var service = new DataService();
            var search = service.GetSearchByString("how");
            Assert.Equal("how to do substring in php", search.First().SearchString);
            Assert.Equal(2, search.Count);
        }

        // Create search with string and userId
        [Fact]
        public void CreateSearchByString_ValidReturn()
        {
            var service = new DataService();
            var newSearch = service.CreateSearchByString(1, "This is a test search");
            Assert.True(newSearch);
        }


        // Get all searches
        [Fact]
        public void GetAllSearches_ValidReturn()
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
