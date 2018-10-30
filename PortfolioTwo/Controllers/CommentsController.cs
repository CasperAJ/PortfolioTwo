using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        public DataService _dataService;

        public CommentsController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetAllComments()
        {
            var comments = _dataService.GetAllComments();

            return Ok(comments);
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


