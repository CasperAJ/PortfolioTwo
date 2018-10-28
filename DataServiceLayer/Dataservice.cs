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

        public List<Comment> GetCommentsByPostId(int id)
        {
            var data = db.Comments
                .Where(x => x.PostId == id);

            return data.ToList();
        }

        //Users
        public User GetUser(int userid)
        {
            return db.Users.FirstOrDefault(x => x.Id == userid);
        }

        // TODO: we need to handle exceptions here.
        public User CreateUser(string username, string password, string email)
        {
            var creationDate = DateTime.Now;
            var id = db.Users.Last().Id;
            id += 1;
            var user = new User() { Id = id, UserName = username, Password = password, Email = email, CreationDate = creationDate };

            db.Users.Add(user);
            db.SaveChanges();

            return user;
        }

        // TODO: we need to handle exceptions here.
        public bool UpdateUser(int id, string email, string password)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return false;
            }

            user.Email = email;
            user.Password = password;
            db.SaveChanges();

            return true;
        }
        

        //Marks
        public List<Mark> GetAllMarksByUser(int userId)
        {
            var markList = db.Marks.Where(x => x.UserId == userId).ToList();
            return markList;
        }

        public List<Mark> GetUserMarkByMarkType(int userId ,int marktypeId)
        {
            var markList = new List<Mark>();
            var query = db.Marks.Where(x => x.UserId == userId && x.Type == marktypeId);
            foreach (var markData in query)
            {
                markList.Add(markData);
            }
            return markList;
        }

        public Mark GetMarkByIdForUser(int postId, int userId)
        {
            return db.Marks.First(x => x.PostId == postId && x.UserId == userId);
        }

        public bool CreateMark(int postId, int userId, int markType, string note)
        {
            var newMark = new Mark();
            newMark.PostId = postId;
            newMark.UserId = userId;
            newMark.Type = markType;
            newMark.Note = note;

            var dataPoint = db.Marks;
            var insertNewMark = dataPoint.Add(newMark);
            db.SaveChanges();
            if (insertNewMark.GetDatabaseValues() != null)
            {
                return true;
            }
            return false;
        }

        public bool DeleteMark(int postId, int userId, int markType)
        {
            var markToDelete = db.Marks.Find(postId, userId, markType);
            //var markToDelete = db.Marks.Where(x => x.PostId == postId && x.UserId == userId && x.Type == markType);
            if (markToDelete != null)
            {
                db.Marks.Remove(markToDelete);
                return true;
            }
            return false;
        }

        //Searches
        public Search GetSearchByString(string wantedSearch)
        {
            //var foundSearch = new Search();

            //var dataSource = db.Searches;
            //var query = dataSource.Where(x => x.SearchString.Equals(wantedSearch))
            //    .Select(x => new {x.SearchString});

            //foreach (var searchData in query)
            //{
            //    foundSearch.SearchString = searchData.SearchString;
            //}

            //return foundSearch;

            return db.Searches.First(x => x.SearchString == wantedSearch);
        }

        public bool CreateSearchByString(int userId, string search)
        {
            var newSearch = new Search();
            newSearch.SearchString = search;
            newSearch.UserId = userId;

            var dataPoint = db.Searches;
            var insertNewSearch = dataPoint.Add(newSearch);
            db.SaveChanges();
            if (insertNewSearch.GetDatabaseValues() != null)
            {
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
            var query = dataSource.Select(x => new { x.Id, x.SearchString, x.UserId });

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
                .Select(x => new { x.Id, x.Name, x.Age, x.Location, x.CreationDate });

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
