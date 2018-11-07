using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioTwo.Models;
using PortfolioTwo.Utility;

namespace PortfolioTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
//    [Authorize]
    public class TagsController : ControllerBase
    {
        private readonly IDataService _dataservice;


        public TagsController(IDataService dataservice)
        {
            _dataservice = dataservice;
        }

        [HttpGet(Name = nameof(GetTags))]
        public IActionResult GetTags(int page=0, int pagesize=10)
        {
            var tags = _dataservice.GetAllTags(page, pagesize);
            var datalist = new List<TagsViewModel>();
            foreach (var tag in tags)
            {
                var model = Mapper.Map<TagsViewModel>(tag);

                datalist.Add(model);
            }

            return Ok(new
            {
                data = datalist,
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetTags), page, pagesize, _dataservice.GetNumberOfTags())
            });

        }

        [HttpGet("{id}", Name = nameof(GetSingleTag))]
        public IActionResult GetSingleTag(int id)
        {
            var tag = _dataservice.GetTagById(id);
            if (tag == null) return NotFound();
            return Ok(Mapper.Map<TagsViewModel>(tag));
            
        }

        [HttpGet("post/{postId}", Name = nameof(GetTagsFromPost))]
        public IActionResult GetTagsFromPost(int postId, int page=0, int pagesize=10)
        {
            var ttp = _dataservice.GetAllTagsFromPostId(postId, page, pagesize);
            var datalist = new List<TagsToPostsViewModel>();
            foreach (var tagToPost in ttp)
            {
                var model = Mapper.Map<TagsToPostsViewModel>(tagToPost);
                model.Tag = LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetSingleTag), tagToPost.TagId);
                model.Post = LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), tagToPost.PostId);

                datalist.Add(model);
            }


            return Ok(new
            {
                data = datalist,
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetTagsFromPost), page, pagesize, _dataservice.GetNumberOfTagsFromPostId(postId))
            });
        }

        [HttpGet("tagtopost/{postId}&{tagId}", Name = nameof(GetSingleTagToPost))]
        public IActionResult GetSingleTagToPost(int postId, int tagId)
        {
            var ttp = _dataservice.GetTagToPostFromId(postId, tagId);
            if (ttp == null) return NotFound();

            var model = Mapper.Map<TagsToPostsViewModel>(ttp);
            model.Tag = LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetSingleTag), ttp.TagId);
            model.Post = LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), ttp.PostId);

            return Ok(model);
        }


    }
}