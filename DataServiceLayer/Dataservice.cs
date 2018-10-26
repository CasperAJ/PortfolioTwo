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

        public Post GetPostById(int id)
        {
            return db.Posts.FirstOrDefault(x => x.Id == id);
        }

        //er ikke sikker hvordan vi gør med answer og questions
        public Post GetAnswerById(int id)
        {
            return null;
        }

        //Comments
        public List<Comment> GetAllComments()
        {
            return db.Comments.OrderBy(x => x.Id).ToList();
        }

        public Comment GetCommentById(int id)
        {
            return db.Comments.FirstOrDefault(x => x.Id == id);
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
        public Search GetSearchByString(string wantedSearch)
        {
            var foundSearch = new Search();

            var dataSource = db.Searches;
            var query = dataSource.Where(x => x.SearchString.Equals(wantedSearch))
                .Select(x => new {x.SearchString});

            foreach (var searchData in query)
            {
                foundSearch.SearchString = searchData.SearchString;
            }

            //return foundSearch;

            return db.Searches.First(x=> x.SearchString == wantedSearch);
        }

        public bool CreateSearchByString(int userId, string search)
        {
            var newSearch = new Search();
            newSearch.SearchString = search;
            newSearch.UserId = userId;


            var dataPoint = db.Searches;
            var insertNewSearch  = dataPoint.Add(newSearch);
            db.SaveChanges();
            if (insertNewSearch.GetDatabaseValues() != null)
            {
                Search newlyInsertedSearch = (Search)insertNewSearch.GetDatabaseValues().ToObject();
                //var delteSearch = dataPoint.Find(newlyInsertedSearch.Id);
                dataPoint.Remove(newlyInsertedSearch);
                return true;
            }
            return false;
        }

        // Seach type ? Search er unitype ?
        public List<Search> GetSearchBySearchType(string searchtype)
        {

            return null;
        }

        public List<Search> GetAllSearches()
        {
            var searchList = new List<Search>();

            var dataSource = db.Searches;
            var query = dataSource.Select(x => new {x.Id, x.SearchString, x.UserId});

            foreach (var searchData in query)
            {
                var searchObj = new Search();
                searchObj.Id = searchData.Id;
                searchObj.SearchString = searchData.SearchString;
                searchObj.UserId = searchData.UserId;
                searchList.Add(searchObj);
            }

            return searchList;
        }

        public List<Search> GetAllSearchesByUserId(int userId)
        {
            var searchList = new List<Search>();

            var dataSource = db.Searches;
            var query = dataSource.Where(x => x.UserId.Equals(userId))
                .Select(x => new { x.Id, x.SearchString, x.UserId });

            foreach (var searchData in query)
            {
                var searchObj = new Search();
                searchObj.Id = searchData.Id;
                searchObj.SearchString = searchData.SearchString;
                searchObj.UserId = searchData.UserId;
                searchList.Add(searchObj);
            }

            return searchList;
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

            return authorObj;
        }

    }
}
