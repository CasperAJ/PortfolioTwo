using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PortfolioTwo.Models;

namespace PortfolioTwo.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        public DataService DataService;

        [HttpGet("id", Name = nameof(GetAuthorById))]
        public IActionResult GetAuthorById(int id)
        {
            var author = DataService.GetAuthor(id);
            if (author == null) return NotFound();

            var ViewModel = Mapper.Map<AuthorViewModel>(author);
            ViewModel.Path = Url.Link(nameof(GetAuthorById), new {id = author.Id});


            return Ok(ViewModel);
        }
    }
}