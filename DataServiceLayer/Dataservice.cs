using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DataServiceLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataServiceLayer
{
    

    public class DataService : IDataService
    {
        private StackOverflowContext db;

        public DataService()
        {
            db = new StackOverflowContext();

        }

        // searches
        // NOTE: try this ==> context.Database.ExecuteSqlCommand("CreateStudents @p0, @p1", parameters: new[] { "Bill", "Gates" });
        public List<Post> SearchExact(string searchterms)
        {
            var terms = searchterms.Split(" ");
            var command = "select * from ExactMatch(";

            var counter = 1;
            foreach (var term in terms)
            {
                if (counter == terms.Length)
                {
                    command = command + "'" + term + "'";
                }
                else
                {
                    command = command + "'" + term + "',";
                }

                counter++;
            }

            command = command + ");";
            var matchresult = db.ExactSearchResults.FromSql(command).ToList();

            var results = db.Posts.Where(x => matchresult.Select(r => r.Id).Contains(x.Id)).ToList();
            return results;

        }

        // NOTE: try this ==> context.Database.ExecuteSqlCommand("CreateStudents @p0, @p1", parameters: new[] { "Bill", "Gates" });
        public List<PostTFIDF> SearchBestTFIDF(string searchterms)
        {
            var terms = searchterms.Split(" ");
            var command = "select * from BestMatchtfidf(";

            var counter = 1;
            foreach (var term in terms)
            {
                if (counter == terms.Length)
                {
                    command = command + "'" + term + "'";
                }
                else
                {
                    command = command + "'" + term + "',";
                }

                counter++;
            }

            command = command + ");";
            var matchresult = db.BestSearchResultTFIDFs.FromSql(command).ToList();
            return matchresult;

        }

        // NOTE: try this ==> context.Database.ExecuteSqlCommand("CreateStudents @p0, @p1", parameters: new[] { "Bill", "Gates" });
        public List<PostRank> SearchBestRank(string searchterms)
        {
            var terms = searchterms.Split(" ");
            var command = "select * from BestMatchSimple(";

            var counter = 1;
            foreach (var term in terms)
            {
                if (counter == terms.Length)
                {
                    command = command + "'" + term + "'";
                }
                else
                {
                    command = command + "'" + term + "',";
                }

                counter++;
            }

            command = command + ");";
            var matchresult = db.BestSearchResultRanks.FromSql(command).ToList();
            return matchresult;

        }



        public List<CloudSimple> WordCloudSimple(string searchterms)
        {

            var terms = searchterms.Split(" ");
            var command = "select * from simpleCloud(";

            var counter = 1;
            foreach (var term in terms)
            {
                if (counter == terms.Length)
                {
                    command = command + "'" + term + "'";
                }
                else
                {
                    command = command + "'" + term + "',";
                }

                counter++;
            }

            command = command + ");";
            var matchresult = db.CloudSimples.FromSql(command).ToList();
            return matchresult;

        }

        public List<CloudTFIDF> WordCloudTFIDF(string searchterms)
        {

            var terms = searchterms.Split(" ");
            var command = "select * from tfidfCloud(";

            var counter = 1;
            foreach (var term in terms)
            {
                if (counter == terms.Length)
                {
                    command = command + "'" + term + "'";
                }
                else
                {
                    command = command + "'" + term + "',";
                }

                counter++;
            }

            command = command + ");";
            var matchresult = db.CloudTfidfs.FromSql(command).ToList();
            return matchresult;

        }

        public List<WordAssociation> WordAssociationSearch(string searchterm)
        {

            var command = "select * from SearchAssoc('" + searchterm + "');";

            var matchresult = db.WordAssociations.FromSql(command).ToList();
            return matchresult;

        }


        //tags

        public List<Tag> GetAllTags(int page, int pagesize)
        {
            return db.Tags
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }

        public Tag GetTagById(int id)
        {
            return db.Tags.FirstOrDefault(x => x.Id == id);
        }

        public List<TagToPost> GetAllTagsFromPostId(int postId, int page, int pagesize)
        {
            return db.TagToPosts.Where(x => x.PostId == postId)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }


        public TagToPost GetTagToPostFromId(int postId, int tagId)
        {
            return db.TagToPosts.Where(x => x.TagId == tagId).FirstOrDefault(x => x.PostId == postId);
        }

        public int GetNumberOfTags()
        {
            return db.Tags.Count();
        }

        public int GetNumberOfTagsFromPostId(int postId)
        {
            return db.TagToPosts.Count(x => x.PostId == postId);
        }


        //Posts
        public List<Post> GetAllPosts(int page = 0, int pagesize = 10)
        {
            return db.Posts.OrderBy(x => x.Id)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }

        public int GetNumberOfPosts()
        {
            return db.Posts.Count();
        }

        public Post GetPostById(int id)
        {
             return db.Posts.FirstOrDefault(x => x.Id == id);
        }

        //NOTE: there is no paging on this one, as of now,
        // its undecided whether paging should be on the questions children,
        // when presenting in the frontend.
        public List<Answer> GetAnswersById(int id)
        {
            return db.Answers.Where(x => x.ParentId == id).ToList();
        }

        public List<Question> GetQuestions(int page = 0, int pagesize = 10)
        {
            return db.Questions
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }

        public int GetNumberOfQuestions()
        {
            return db.Questions.Count();
        }

        public Question GetQuestionById(int id)
        {
            return db.Questions.FirstOrDefault(x => x.Id == id);
        }

        //Comments
        public List<Comment> GetAllComments(int page=0, int pagesize=10)
        {
            return db.Comments
                .OrderBy(x => x.Id)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }

        public int GetNumberOfComments()
        {
            return db.Comments.Count();
        }

        public Comment GetCommentById(int id)
        {
            return db.Comments.FirstOrDefault(x => x.Id == id);
        }

        public List<Comment> GetCommentsByPostId(int id, int page = 0, int pagesize = 10)
        {
            var data = db.Comments
                .Where(x => x.PostId == id)
                .Skip(page * pagesize)
                .Take(pagesize);

            return data.ToList();
        }

        public int GetNumberOfCommentsByPostId(int id)
        {
            return db.Comments.Count(x => x.PostId == id);
        }

        //Users
        public User GetUser(int userid)
        {
            return db.Users.FirstOrDefault(x => x.Id == userid);
        }

        public User GetUserByUserName(string userName)
        {
            return db.Users.FirstOrDefault(x => x.UserName == userName);
        }


        public User CreateUser(string username, string password, string salt, string email)
        {
            var creationDate = DateTime.Now;
            var user = new User()
            {
                UserName = username,
                Password = password,
                Salt = salt,
                Email = email,
                CreationDate = creationDate
            };

            db.Users.Add(user);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return null;
            }
            

            return user;
        }

        public bool UpdateUser(int id, string email, string password)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return false;
            }

            user.Email = email;
            user.Password = password;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            

            return true;
        }


        //Marks
        public List<Mark> GetAllMarksByUser(int userId, int page = 0, int pagesize = 10)
        {
            var markList = db.Marks.Where(x => x.UserId == userId)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
            return markList;
        }

        public int GetNumberOfMarksByUser(int id)
        {
            return db.Marks.Count(x => x.UserId == id);
        }

        public List<Mark> GetUserMarkByMarkType(int userId, int marktypeId, int page = 0, int pagesize = 10)
        {
            var markList = new List<Mark>();
            var query = db.Marks.Where(x => x.UserId == userId && x.Type == marktypeId).Skip(page * pagesize).Take(pagesize);
            foreach (var markData in query)
            {
                markList.Add(markData);
            }

            return markList;
        }

        public Mark GetMarkByIdForUser(int postId, int userId, int markTypeId)
        {
            return db.Marks.First(x => x.PostId == postId && x.UserId == userId && x.Type == markTypeId);
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
            var markToDelete = db.Marks.FirstOrDefault(x => x.PostId == postId && x.UserId == userId && x.Type == markType);
            if (markToDelete != null)
            {
                db.Marks.Remove(markToDelete);
                return true;
            }
            return false;
        }

        //Searches
        public List<Search> GetSearchByString(string wantedSearch, int page = 0, int pagesize = 10)
        {
           return db.Searches.Where(x => x.SearchString.ToLower().Contains(wantedSearch.ToLower()))
               .Skip(page * pagesize)
               .Take(pagesize)
               .ToList();       
        }

        public int GetNumberOfSearchesByString(string wantedSearch)
        {
            return db.Searches.Count(x => x.SearchString.ToLower().Contains(wantedSearch.ToLower()));
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

        public List<Search> GetAllSearches(int page = 0, int pagesize = 10)
        {
            var searchList = new List<Search>();

            var dataSource = db.Searches;
            var query = dataSource.Select(x => new { x.Id, x.SearchString, x.UserId, x.User }).Skip(page * pagesize).Take(pagesize);

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

        public int GetNumberOfSearches()
        {
            return db.Searches.Count();
        }

        public List<Search> GetAllSearchesByUserId(int userId, int page = 0, int pagesize = 10)
        {
            var searchList = new List<Search>();

            var dataSource = db.Searches;
            var query = dataSource.Where(x => x.UserId.Equals(userId))
                .Select(x => new { x.Id, x.SearchString, x.UserId})
                .Skip(page * pagesize)
                .Take(pagesize);

            foreach (var searchData in query)
            {
                var searchObj = new Search();
                searchObj.Id = searchData.Id;
                searchObj.SearchString = searchData.SearchString;
                searchObj.UserId = searchData.UserId;
                searchList.Add(searchObj);
            }

            if (searchList.Count == 0)
            {
                return null;
            }

            return searchList;
        }

        public int GetNumberOfSearchByUser(int id)
        {
            return db.Searches.Count(x => x.UserId == id);
        }

        //Author
        public Author GetAuthor(int authorId)
        {
            var authorObj = new Author();
            authorObj.Id = 0;

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

            if (authorObj.Id == 0) return null;


            return authorObj;
        }

    }
}
