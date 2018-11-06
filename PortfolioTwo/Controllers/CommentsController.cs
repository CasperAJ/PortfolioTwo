using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DataServiceLayer.Models;
using Microsoft.AspNetCore.Authorization;
using PortfolioTwo.Models;
using PortfolioTwo.Utility;

namespace PortfolioTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {

        public IDataService _dataService;

        public CommentsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet(Name = nameof(GetAllComments))]
        public IActionResult GetAllComments(int page = 0, int pagesize = 10)
        {
            var comments = _dataService.GetAllComments(page, pagesize);
            List<CommentViewModel> Commentslist = new List<CommentViewModel>();
            
            foreach (var comment in comments)
            {
                var toadd = Mapper.Map<CommentViewModel>(comment);

                toadd.Path = LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetCommentById), comment.Id);
                toadd.Post = LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), comment.PostId);
                toadd.Author = LinkBuilder.CreateIdentityLink(Url.Link, nameof(AuthorController.GetAuthorById),
                    comment.AuthorId);
                Commentslist.Add(toadd);
            }

            var returnobj = new
            {
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetAllComments), page, pagesize, _dataService.GetNumberOfComments()),
                data = Commentslist
            };

            return Ok(returnobj);
        }

        [HttpGet("{id}", Name = nameof(GetCommentById))]
        public IActionResult GetCommentById(int id)
        {
            var comment = _dataService.GetCommentById(id);

            var viewmodel = Mapper.Map<CommentViewModel>(comment);

            viewmodel.Post = LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), comment.PostId);
            viewmodel.Author = LinkBuilder.CreateIdentityLink(Url.Link, nameof(AuthorController.GetAuthorById),
                comment.AuthorId);

            return Ok(viewmodel);
        }

        [HttpGet("postid/{id}", Name = nameof(GetCommentsByPostId))]
        public IActionResult GetCommentsByPostId(int id, int page = 0, int pagesize = 10)
        {
            var comments = _dataService.GetCommentsByPostId(id, page, pagesize);


            List<CommentViewModel> Commentslist = new List<CommentViewModel>();

            foreach (var comment in comments)
            {
                var toadd = Mapper.Map<CommentViewModel>(comment);

                //TODO: Remember to delete line....
                //toadd.Post = Url.Link(nameof(PostsController.GetSingle), new { id = comment.PostId});
                toadd.Path = LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetCommentById), comment.Id);
                toadd.Post = LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), comment.PostId);
                toadd.Author = LinkBuilder.CreateIdentityLink(Url.Link, nameof(AuthorController.GetAuthorById),
                    comment.AuthorId);
                Commentslist.Add(toadd);
            }

            var returnobj = new
            {
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetCommentsByPostId), page, pagesize, _dataService.GetNumberOfCommentsByPostId(id)),
                data = Commentslist
            };


            return Ok(returnobj);
        }

    }
}


