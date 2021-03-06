﻿using System;
using System.Collections.Generic;
using System.Text;
using DataServiceLayer.Models;

namespace DataServiceLayer
{
    public interface IDataService
    {
        List<Post> GetAllPosts(int page, int pagesize);
        int GetNumberOfPosts();
        Post GetPostById(int id);
        List<Answer> GetAnswersById(int id);
        List<Question> GetQuestions(int page=0, int pagesize=10);
        int GetNumberOfQuestions();
        Question GetQuestionById(int id);
        List<Comment> GetAllComments(int page, int pagesize);
        int GetNumberOfComments();
        Comment GetCommentById(int id);
        List<Comment> GetCommentsByPostId(int id, int page, int pagesize);
        int GetNumberOfCommentsByPostId(int id);
        User GetUser(int userid);
        User GetUserByUserName(string userName);
        User CreateUser(string username, string password, string salt, string email);
        bool UpdateUser(int id, string email, string password);
        List<Mark> GetAllMarksByUser(int userId, int page, int pagesize);
        int GetNumberOfMarksByUser(int id);
        List<Mark> GetUserMarkByMarkType(int userId, int marktypeId, int page, int pagesize);
        Mark GetMarkByIdForUser(int postId, int userId, int marktypeId);
        bool CreateMark(int postId, int userId, int markType, string note);
        bool DeleteMark(int postId, int userId, int markType);
        List<Search> GetSearchByString(string wantedSearch, int page, int pagesize);
        int GetNumberOfSearchesByString(string wantedSearch);
        bool CreateSearchByString(int userId, string search);
        List<Search> GetAllSearches(int page, int pagesize);
        int GetNumberOfSearches();
        List<Search> GetAllSearchesByUserId(int userId, int page, int pagesize);
        int GetNumberOfSearchByUser(int id);
        Author GetAuthor(int authorId);

        List<Tag> GetAllTags(int page, int pagesize);
        Tag GetTagById(int id);
        List<TagToPost> GetAllTagsFromPostId(int postId, int page, int pagesize);
        int GetNumberOfTags();
        int GetNumberOfTagsFromPostId(int postId);
        TagToPost GetTagToPostFromId(int postId, int tagId);

        List<Post> SearchExact(string search, int userid, int page, int pagesize, out int numberOfResults);
        List<PostTFIDF> SearchBestTFIDF(string searchterms, int userid, int page, int pagesize, out int numberOfResults);
        List<PostRank> SearchBestRank(string searchterms, int userid, int page, int pagesize, out int numberOfResults);

        List<CloudSimple> WordCloudSimple(string searchterms);
        List<CloudTFIDF> WordCloudTFIDF(string searchterms);

        List<WordAssociation> WordAssociationSearch(string searchterm);

        List<ForceNode> ForceGraph(string term, int grade);
    }
}
