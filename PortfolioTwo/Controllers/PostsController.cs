using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioTwo.Controllers
{
    // TODO: implements viewmodels here.
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private IDataService _dataservice;

        public PostsController(IDataService dataservice)
        {
            _dataservice = dataservice;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var posts = _dataservice.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var post = _dataservice.GetPostById(id);
            if (post == null) return NotFound();

            return Ok(post);
        }


        [HttpGet]
        [Route("{id}/children")]
        public IActionResult GetAnswers(int id)
        {
            var posts = _dataservice.GetAnswerById(id);
            return Ok(posts);
        }




    }
}
