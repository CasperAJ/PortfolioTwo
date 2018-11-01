using System;
using System.Collections.Generic;
using System.Text;
using DataServiceLayer.Models;

namespace DataServiceLayer
{
    public interface IDataService
    {
        List<Post> GetAllPosts(int page, int pagesize);
        Post GetPostById(int id);
        Post GetAnswerById(int id);
        List<Comment> GetAllComments(int page, int pagesize);
        Comment GetCommentById(int id);
        List<Comment> GetCommentsByPostId(int id, int page, int pagesize);
        User GetUser(int userid);
        User GetUserByUserName(string userName);
        User CreateUser(string username, string password, string salt, string email);
        bool UpdateUser(int id, string email, string password);
        List<Mark> GetAllMarksByUser(int userId, int page, int pagesize);
        List<Mark> GetUserMarkByMarkType(int userId, int marktypeId, int page, int pagesize);
        Mark GetMarkByIdForUser(int postId, int userId, int marktypeId);
        bool CreateMark(int postId, int userId, int markType, string note);
        bool DeleteMark(int postId, int userId, int markType);
        List<Search> GetSearchByString(string wantedSearch, int page, int pagesize);
        bool CreateSearchByString(int userId, string search);
        List<Search> GetAllSearches(int page, int pagesize);
        List<Search> GetAllSearchesByUserId(int userId, int page, int pagesize);
        Author GetAuthor(int authorId);
    }
}
