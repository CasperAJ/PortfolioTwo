using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            return db.Posts.OrderBy(x => x.Id).ToList();
        }

        public List<Post> GetPostById(int id)
        {
            return null;
        }

        public Post GetAnswerById(int id)
        {
            return null;
        }

        //Comments
        public List<Comment> GetAllComments()
        {
            return null;
        }

        public Comment GetCommentById(int id)
        {
            return null;
        }

        public Comment GetCommentByPostId(int id)
        {
            return null;
        }

        //Users
        public User GetUser(int id)
        {
            return null;
        }

        public bool CreateUser(User user)
        {
            return false;
        }

        /*
        public bool UpdateUser(User user)
        {
            return false;
        }
        */

        //Marks
        public Mark GetAllMarks()
        {
            return null;
        }

        public Mark GetMarkById(int id)
        {
            return null;
        }

        public bool CreateMark(int postId, int userId)
        {
            return false;
        }

        public bool DeleteMark(int postId, int userId)
        {
            return false;
        }

        //Searches
        public Search GetSearchByString(string search)
        {
            return null;
        }

        public bool CreateSearchByString(int userId, string search)
        {
            return false;
        }

        public List<Search> GetSearchBySearchType(string searchtype)
        {
            return null;
        }

        public List<Search> GetAllSearches(string search)
        {
            return null;
        }

        public Search GetSearchById(int id)
        {
            return null;
        }

        //Author
        public Author GetAuthor(int authorId)
        {
            var authorObj = new Author();

            var dataSource = db.Authors;
            var lingQuery = dataSource.Where(x => x.Id.Equals(authorId))
                .Select(x => new {x.Id, x.Name, x.Age, x.Location, x.CreationDate});

            foreach (var authorData in lingQuery)
            {
                authorObj.Id = authorData.Id;
                authorObj.Name = authorData.Name;
                authorObj.Age = authorData.Age;
                authorObj.Location = authorData.Location;
                authorObj.CreationDate = authorData.CreationDate;
            }

            return null;
        }

    }
}
