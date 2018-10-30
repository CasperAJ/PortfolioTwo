using System;
using System.Collections.Generic;
using System.Text;
using DataServiceLayer.Models;

namespace DataServiceLayer
{
    public interface IDataService
    {
        List<Post> GetAllPosts();
        Post GetPostById(int id);
        Post GetAnswerById(int id);
        List<Comment> GetAllComments(int page, int pagesize);
        Comment GetCommentById(int id);
        List<Comment> GetCommentsByPostId(int id);
        User GetUser(int userid);
        User CreateUser(string username, string password, string email);
        bool UpdateUser(int id, string email, string password);
        List<Mark> GetAllMarksByUser(int userId);
        List<Mark> GetUserMarkByMarkType(int userId, int marktypeId);
        Mark GetMarkByIdForUser(int postId, int userId);
        bool CreateMark(int postId, int userId, int markType, string note);
        bool DeleteMark(int postId, int userId, int markType);
        List<Search> GetSearchByString(string wantedSearch);
        bool CreateSearchByString(int userId, string search);
        List<Search> GetAllSearches();
        List<Search> GetAllSearchesByUserId(int userId);
        Author GetAuthor(int authorId);
    }
}
