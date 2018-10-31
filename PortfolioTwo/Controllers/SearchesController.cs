using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLayer;
using DataServiceLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioTwo.Models;
using PortfolioTwo.Utility;

namespace PortfolioTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchesController : Controller
    {
        private IDataService _dataservice;

        public SearchesController(IDataService dataservice)
        {
            _dataservice = dataservice;
        }

        [HttpGet(Name = nameof(GetAllSearches))]
        public IActionResult GetAllSearches(int page = 0, int pagesize = 10)
        {
            var searches = _dataservice.GetAllSearches(page, pagesize);
            List<SearchViewModel> searchList = new List<SearchViewModel>();

            foreach (var search in searches)
            {
                var toAdd = Mapper.Map<SearchViewModel>(search);
                toAdd.Path = LinkBuilder.CreateIdentityLink(Url.Link, nameof(SearchesController.GetAllSearches), search.Id);
                searchList.Add(toAdd);
            }

            var returnobj = new
            {
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetAllSearches), page, pagesize),
                data = searchList
            };

            return Ok(returnobj);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSearchesForUser(int id, int page = 0, int pagesize = 10) 
        {
            var searches = _dataservice.GetAllSearchesByUserId(id, page, pagesize);
            List<SearchViewModel> searchList = new List<SearchViewModel>();

            if (searches == null)
            {
                return NotFound();
            }

            foreach (var search in searches)
            {
                var toAdd = Mapper.Map<SearchViewModel>(search);
                toAdd.Path = LinkBuilder.CreateIdentityLink(Url.Link, nameof(SearchesController.GetSearchesForUser), search.Id);
                searchList.Add(toAdd);
            }

            var returnobj = new
            {
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetSearchesForUser), page, pagesize),
                data = searchList
            };
            return Ok(returnobj);
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
