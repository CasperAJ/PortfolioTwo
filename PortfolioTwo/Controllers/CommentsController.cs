using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DataServiceLayer.Models;
using PortfolioTwo.Models;
using PortfolioTwo.Utility;

namespace PortfolioTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

                //TODO: Remember to delete line....
                //toadd.Post = Url.Link(nameof(PostsController.GetSingle), new { id = comment.PostId});

                toadd.Post = LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), comment.PostId);
                toadd.Author = LinkBuilder.CreateIdentityLink(Url.Link, nameof(AuthorController.GetAuthorById),
                    comment.AuthorId);
                Commentslist.Add(toadd);
            }

            var returnobj = new
            {
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetAllComments), page, pagesize),
                data = Commentslist
            };

            return Ok(returnobj);
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            var comment = _dataService.GetCommentById(id);

            return Ok(comment);
        }

        [HttpGet("postid/{id}")]
        public IActionResult GetCommentsByPostId(int id, int page = 0, int pagesize = 10)
        {
            var comment = _dataService.GetCommentsByPostId(id, page, pagesize);

            return Ok(comment);
        }

    }
}


