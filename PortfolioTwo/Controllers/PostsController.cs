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

        //[Authorize]
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
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(Get), page, pagesize),
                data = postsList
            };

            return Ok(returnobj);
        }

        [HttpGet]
        [Route("{id}", Name = nameof(GetSingle))]
        public IActionResult GetSingle(int id)
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
