using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLayer;
using DataServiceLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTwo.Models;
using PortfolioTwo.Utility;

namespace PortfolioTwo.Controllers
{
    [Route("api/markings")]
    [ApiController]
    [Authorize]
    public class MarkingsController : ControllerBase
    {
        private IDataService _dataservice;

        public MarkingsController(IDataService dataservice)
        {
            _dataservice = dataservice;
        }


        [HttpGet("{postid}", Name = nameof(GetMarkById))]
        public IActionResult GetMarkById(int postId)
        {
            var userid = User.Identity.Name;

            var mark = _dataservice.GetMarkByIdForUser(postId, Convert.ToInt32(userid), 1);
            if (mark == null) return NotFound();

            var model = Mapper.Map<MarkingViewModel>(mark);

            model.Post = LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), mark.PostId);


            return Ok(model);
        }


        [HttpGet]
        [Route("marks/user", Name = nameof(GetAllMarksByUserId))]
        public IActionResult GetAllMarksByUserId(int page = 0, int pagesize = 10)
        {
            var userid = User.Identity.Name;
            var markings = _dataservice.GetAllMarksByUser(Convert.ToInt32(userid), page, pagesize);
            List<MarkingViewModel> markingsList = new List<MarkingViewModel>();

            foreach (var mark in markings)
            {
                var markToAdd = Mapper.Map<MarkingViewModel>(mark);
                markToAdd.Post =
                LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), mark.PostId);

                markToAdd.PostTitle = mark.Post.Title;


                markingsList.Add(markToAdd);
            }

            var returnObj = new
            {
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetAllMarksByUserId), page, pagesize, _dataservice.GetNumberOfMarksByUser(Convert.ToInt32(userid))),
                data = markingsList
            };

            return Ok(returnObj);


        }


        [HttpPost]
        public IActionResult Create(Mark mark)
        {
            var userid = User.Identity.Name;

            if (mark.PostId == 0)
            {
                return BadRequest();
            }

            var newmark = _dataservice.CreateMark(mark.PostId, Convert.ToInt32(userid), 1, mark.Note);

            if (!newmark)
            {
                return StatusCode(500);
            }

            return Created("", newmark);
        }


        [HttpDelete]
        public IActionResult Delete(Mark mark)
        {
            var userid = User.Identity.Name;
            var check = _dataservice.DeleteMark(mark.PostId, Convert.ToInt32(userid), 1);
            if (check) return Ok();
            return NotFound();
        }





    }
}
