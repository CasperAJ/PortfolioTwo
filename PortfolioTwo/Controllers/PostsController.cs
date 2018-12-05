using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTwo.Models;
using PortfolioTwo.Utility;

namespace PortfolioTwo.Controllers
{

    [Route("api/posts")]
    [ApiController]
    //[Authorize]
    public class PostsController : ControllerBase
    {

        private IDataService _dataservice;

        public PostsController(IDataService dataservice)
        {
            _dataservice = dataservice;
        }

        [HttpGet(Name = nameof(Get))]
        public IActionResult Get(int page = 0, int pagesize = 10)
        {
            var posts = _dataservice.GetAllPosts(page, pagesize);
            List<PostViewModel> postsList = new List<PostViewModel>();

            foreach (var post in posts)
            {
                var toAdd = Mapper.Map<PostViewModel>(post);
                toAdd.path = LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.Get), post.Id);
                toAdd.Author = LinkBuilder.CreateIdentityLink(Url.Link, nameof(AuthorController.GetAuthorById), post.AuthorId);
                postsList.Add(toAdd);
            }

            var returnobj = new
            {
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(Get), page, pagesize, _dataservice.GetNumberOfPosts()),
                data = postsList
            };

            return Ok(returnobj);
        }

        [HttpGet("{id}", Name = nameof(GetSingle))]
        public IActionResult GetSingle(int id)
        {
            var post = _dataservice.GetPostById(id);
            if (post == null) return NotFound();

            var model = Mapper.Map<PostViewModel>(post);

            model.AcceptedAnswer = post.AcceptedAnswerId == null
                ? null
                : LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetSingle), (int)post.AcceptedAnswerId);
            model.Parent = post.ParentId == null ? null : LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetSingle), (int)post.ParentId);
            model.LinkPost = post.LinkPostId == null ? null : LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetSingle), (int)post.LinkPostId);

            return Ok(model);
        }

        /// <summary>
        /// Get the children (answers) to a given post.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/children", Name = nameof(GetAnswers))]
        public IActionResult GetAnswers(int id)
        {
            var answers = _dataservice.GetAnswersById(id);

            var viewList = new List<AnswerViewModel>();

            foreach (var answer in answers)
            {
                var model = Mapper.Map<AnswerViewModel>(answer);

                // parent id will (should) never be null on answers.
                model.Parent = LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetSingle), (int)answer.ParentId);
                model.Comments = LinkBuilder.CreateIdentityLink(Url.Link,
                    nameof(CommentsController.GetCommentsByPostId), answer.Id);

                viewList.Add(model);
            }

            return Ok(viewList);
        }

        [HttpGet("/questions", Name = nameof(GetQuestions))]
        public IActionResult GetQuestions(int page = 0, int pagesize = 10)
        {
            var questions = _dataservice.GetQuestions(page, pagesize);

            var listOfQuestions = new List<QuestionViewModel>();

            foreach (var question in questions)
            {
                var model = Mapper.Map<QuestionViewModel>(question);

                model.Answers = LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetAnswers), question.Id);
                model.AcceptedAnswer = question.AcceptedAnswerId == null
                    ? null : LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetSingle), (int)question.AcceptedAnswerId);
                model.LinkPost = question.LinkPostId == null
                    ? null : LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetSingle), (int)question.LinkPostId);

                listOfQuestions.Add(model);
            }

            var returnObj = new
            {
                data = listOfQuestions,
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetQuestions), page, pagesize, _dataservice.GetNumberOfQuestions())
            };


            return Ok(returnObj);

        }




    }
}
