using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLayer;
using DataServiceLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortfolioTwo.Models;
using PortfolioTwo.Services;
using PortfolioTwo.Utility;

namespace PortfolioTwo.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IDataService _dataService;
        private readonly IConfiguration _configuration;

        public UsersController(IDataService dataService, IConfiguration configuration)
        {
            _dataService = dataService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id}", Name = nameof(GetUsers))]
        public IActionResult GetUsers(int id)
        {
            var user = _dataService.GetUser(id);
            if (user == null) return NotFound();
            var model = Mapper.Map<UserViewModel>(user);
            model.path = LinkBuilder.CreateIdentityLink(Url.Link, nameof(UsersController.GetUsers), user.Id);







            return Ok(model);
        }


        [HttpPost]
        public IActionResult Create([FromBody]UserViewModel user)
        {

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest();
            }

            int.TryParse(_configuration["security:pwdsize"], out var size);
            var salt = PasswordService.GenerateSalt(size);
            var pwd = PasswordService.HashPassword(user.Password, salt, size);

            var newuser = _dataService.CreateUser(user.UserName, pwd, salt, user.Email);

            if (newuser == null) return StatusCode(500);

            return Created("",newuser);
        }

        //Note: later on password updates will be handling differently.
        //this will suffice for now.
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, UserViewModel user)
        {
            if (_dataService.UpdateUser(id, user.Email, user.Password))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]UserViewModel user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest();
            }

            int.TryParse(_configuration["security:pwdsize"], out var size);

            if (size == 0)
            {
                //upsuser
                return BadRequest();
            }

            var getUser = _dataService.GetUserByUserName(user.UserName);
            if (getUser == null) return BadRequest();

            var pwd = PasswordService.HashPassword(user.Password, getUser.Salt, size);
            if (pwd != getUser.Password) return BadRequest();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["security:key"]);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, getUser.Id.ToString()),
                }),
                Expires = DateTime.Now.AddMonths(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescription));

            var response = new
            {
                getUser.UserName,
                token
            };
            return Ok(response);
        }


    }
}
