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

        //TODO: routing doesnt make sense. fix.
        [HttpGet("{id}",Name = nameof(GetMarkById))]
        public IActionResult GetMarkById(int postId, int userId, int marktypeId)
        {
            var mark = _dataservice.GetMarkByIdForUser(postId, userId, marktypeId);
            if (mark == null) return NotFound();

            var model = Mapper.Map<MarkingViewModel>(mark);

            model.Post = LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), mark.PostId);


            return Ok(model);
        }

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

            if (mark.PostId == 0 || mark.UserId == 0 || mark.Type == 0)
            {
                return BadRequest();
            }

            var newmark = _dataservice.CreateMark(mark.PostId, mark.UserId, mark.Type, mark.Note);

            if (!newmark)
            {
                return StatusCode(500);
            }

            return Created("", newmark);
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Mark mark)
        {
            var check = _dataservice.DeleteMark(mark.PostId, mark.UserId, mark.Type);
            if (check) return Ok();
            return NotFound();
        }





    }
}
