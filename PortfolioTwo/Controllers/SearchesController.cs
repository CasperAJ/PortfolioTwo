using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using DataServiceLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PortfolioTwo.Controllers
{
    [Route("api/searches")]
    [ApiController]
    public class SearchesController : Controller
    {
        private IDataService _dataservice;

        public SearchesController(IDataService dataservice)
        {
            _dataservice = dataservice;
        }

        [HttpGet]
        public IActionResult GetAllSearches(int page = 0, int pagesize = 10)
        {
            var searches = _dataservice.GetAllSearches(page, pagesize);
            return Ok(searches);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSearchesForUser(int id, int page = 0, int pagesize = 10) 
        {
            var searches = _dataservice.GetAllSearchesByUserId(id, page, pagesize);

            if (searches == null)
            {
                return NotFound();
            }
            return Ok(searches);
        }

        [HttpGet]
        [Route("searchstring/{wantedsearch}")]
        public IActionResult GetSearchBySearchString(string wantedsearch, int page = 0, int pagesize = 10)
        {
            var data = _dataservice.GetSearchByString(wantedsearch, page, pagesize);

            if (data.Count == 0)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreateSearch(Search search)
        {
            _dataservice.CreateSearchByString(search.UserId, search.SearchString);

            return Ok();
        }


    }
}
