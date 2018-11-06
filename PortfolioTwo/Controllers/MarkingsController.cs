using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLayer;
using DataServiceLayer.Models;
using Microsoft.AspNetCore.Mvc;
using PortfolioTwo.Models;
using PortfolioTwo.Utility;

namespace PortfolioTwo.Controllers
{
    [Route("api/markings")]
    [ApiController]
    public class MarkingsController : ControllerBase
    {
        private IDataService _dataservice;

        public MarkingsController(IDataService dataservice)
        {
            _dataservice = dataservice;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetMarkById(int postId, int userId, int marktypeId)
        {
            var markings = _dataservice.GetMarkByIdForUser(postId, userId, marktypeId);
            return Ok(markings);
        }

        [HttpGet]
        [Route("{id}", Name = nameof(GetAllMarksByUserId))]
        public IActionResult GetAllMarksByUserId(int userId, int page = 0, int pagesize = 10)
        {
            var markings = _dataservice.GetAllMarksByUser(userId, page, pagesize);
            List<MarkingViewModel> markingsList = new List<MarkingViewModel>();

            foreach (var mark in markings)
            {
                var markToAdd = Mapper.Map<MarkingViewModel>(mark);
                markToAdd.Post =
                    LinkBuilder.CreateIdentityLink(Url.Link, nameof(PostsController.GetSingle), mark.PostId);
                //markToAdd.Path = LinkBuilder.CreateIdentityLink(Url.Link, nameof(GetMarkById), )
                markingsList.Add(markToAdd);
            }

            var returnObj = new
            {
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetAllMarksByUserId), page, pagesize, _dataservice.GetNumberOfMarksByUser(userId)),
                data = markingsList
            };

            return Ok(returnObj);


        }

        // TODO: implement exception checking here.
        [HttpPost]
        public IActionResult Create(Mark mark)
        {
            var newmark = _dataservice.CreateMark(mark.PostId, mark.UserId, mark.Type, mark.Note);
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
