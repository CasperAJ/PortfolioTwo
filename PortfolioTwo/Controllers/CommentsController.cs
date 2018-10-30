using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PortfolioTwo.Models;

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

        [HttpGet]
        public IActionResult GetAllComments()
        {
            var comments = _dataService.GetAllComments().Take(10);
            List<CommentViewModel> Commentslist = new List<CommentViewModel>();

            foreach (var comment in comments)
            {
                var toadd = Mapper.Map<CommentViewModel>(comment);
                toadd.Post = Url.Link("GetSinglePost", new {id = comment.PostId});
                Commentslist.Add(toadd);
            }

            var returnobj = new
            {
                next = "test",
                prev = "test",
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
        public IActionResult GetCommentsByPostId(int id)
        {
            var comment = _dataService.GetCommentsByPostId(id);

            return Ok(comment);
        }

    }
}


