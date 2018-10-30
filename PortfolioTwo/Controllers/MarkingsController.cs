﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using DataServiceLayer.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get(int id, int page = 0, int pagesize = 10)
        {
            var markings = _dataservice.GetAllMarksByUser(id, page, pagesize);
            return Ok(markings);

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
