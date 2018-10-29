using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using DataServiceLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioTwo.Controllers
{
    [Route("api/searches")]
    [ApiController]
    public class SearchesController : Controller
    {
        private DataService _dataservice;

        public SearchesController(DataService dataservice)
        {
            _dataservice = dataservice;
        }

        [HttpGet]
        public IActionResult GetAllSearches()
        {
            var searches = _dataservice.GetAllSearches();
            return Ok(searches);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSearchesForUser(int id)
        {
            var searches = _dataservice.GetAllSearchesByUserId(id);

            if (searches == null)
            {
                return NotFound();
            }
            return Ok(searches);
        }

        [HttpPost]
        public IActionResult CreateSearch(Search search)
        {
            _dataservice.CreateSearchByString(search.UserId, search.SearchString);

            return Ok();
        }
    }
}
