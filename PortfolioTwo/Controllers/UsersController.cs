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
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IDataService _dataservice;

        public UsersController(IDataService dataservice)
        {
            _dataservice = dataservice;
        }

        [HttpGet]
        [Route("{id}", Name = nameof(GetUsers))]
        public IActionResult GetUsers(int id)
        {
            var user = _dataservice.GetUser(id);
            if (user == null) return NotFound();
            var model = Mapper.Map<UserViewModel>(user);
            //model.path = Url.Link(nameof(GetUsers))
            model.path = LinkBuilder.CreateIdentityLink(Url.Link, nameof(UsersController.GetUsers), user.Id);







            return Ok(model);
        }

        // TODO: change these to use viewmodels later with automapper.
        // TODO: when exception handling is added to the dataservice, add badrequest return here.
        [HttpPost]
        public IActionResult Create(User user)
        {
            var newuser = _dataservice.CreateUser(user.UserName, user.Password, user.Email);


            return Created("",newuser);
        }

        // TODO: change these to use viewmodels later with automapper.
        // TODO: when exception handling is added to the dataservice, add badrequest return here.
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, User user)
        {
            if (_dataservice.UpdateUser(user.Id, user.Email, user.Password))
            {
                return Ok();
            }

            return BadRequest();
        }


    }
}
