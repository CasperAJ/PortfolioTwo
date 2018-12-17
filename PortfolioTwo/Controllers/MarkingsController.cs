using System;
using System.Collections.Generic;
using System.Linq;
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
    //[Authorize]
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
            //var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userid = 2;

            var mark = _dataservice.GetMarkByIdForUser(postId, userid, 1);
            if (mark == null) return NotFound();

            var model = Mapper.Map<MarkingViewModel>(mark);

            model.Post = LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), mark.PostId);


            return Ok(model);
        }


        //TODO: doesnt need userid in route.
        [HttpGet]
        [Route("{id}/user", Name = nameof(GetAllMarksByUserId))]
        public IActionResult GetAllMarksByUserId(int id, int page = 0, int pagesize = 10)
        {
            var markings = _dataservice.GetAllMarksByUser(id, page, pagesize);
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
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetAllMarksByUserId), page, pagesize, _dataservice.GetNumberOfMarksByUser(id)),
                data = markingsList
            };

            return Ok(returnObj);


        }


        [HttpPost]
        public IActionResult Create(Mark mark)
        {
            //var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userid = 2;

            if (mark.PostId == 0)
            {
                return BadRequest();
            }

            var newmark = _dataservice.CreateMark(mark.PostId, userid, 1, mark.Note);

            if (!newmark)
            {
                return StatusCode(500);
            }

            return Created("", newmark);
        }


        [HttpDelete]
        public IActionResult Delete(Mark mark)
        {
            //var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userid = 2;
            var check = _dataservice.DeleteMark(mark.PostId, userid, 1);
            if (check) return Ok();
            return NotFound();
        }





    }
}
