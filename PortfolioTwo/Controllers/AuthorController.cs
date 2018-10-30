using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioTwo.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        public DataService DataService;

        [HttpGet("id")]
        public IActionResult GetAuthorById(int id)
        {
            var author = DataService.GetAuthor(id);
            if (author == null) return NotFound();
            return Ok(author);
        }
    }
}