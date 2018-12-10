using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLayer;
using DataServiceLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortfolioTwo.Models;
using PortfolioTwo.Utility;

namespace PortfolioTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetAllSearches), page, pagesize, _dataservice.GetNumberOfSearches()),
                data = searchList
            };

            return Ok(returnobj);
        }

        [HttpGet("{id}", Name = nameof(GetSearchesForUser))]
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
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetSearchesForUser), page, pagesize, _dataservice.GetNumberOfSearchByUser(id)),
                data = searchList
            };
            return Ok(returnobj);
        }

        [HttpGet("searchstring/{wantedsearch}", Name = nameof(GetSearchBySearchString))]
        public IActionResult GetSearchBySearchString(string wantedsearch, int page = 0, int pagesize = 10)
        {
            var data = _dataservice.GetSearchByString(wantedsearch, page, pagesize);
            List<SearchViewModel> searchList = new List<SearchViewModel>();

            if (data.Count == 0)
            {
                return NotFound();
            }

            foreach (var search in data)
            {
                var toAdd = Mapper.Map<SearchViewModel>(search);
                toAdd.Path = LinkBuilder.CreateIdentityLink(Url.Link, nameof(SearchesController.GetSearchBySearchString), search.Id);
                searchList.Add(toAdd);
            }

            var returnobj = new
            {
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(GetSearchBySearchString), page, pagesize, _dataservice.GetNumberOfSearchesByString(wantedsearch)),
                data = searchList
            };

            return Ok(returnobj);
        }

        [HttpPost]
        public IActionResult CreateSearch(Search search)
        {
            _dataservice.CreateSearchByString(search.UserId, search.SearchString);

            return Ok();
        }




        
        [HttpPost("bestrank")]
        public IActionResult BestRank(Search search, int page = 0, int pagesize = 10)
        {
            //var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userid = "2";
            var results = _dataservice.SearchBestRank(search.SearchString, int.Parse(userid), page, pagesize);

            var returnobj = new
            {
                paging = LinkBuilder.CreatePageLink(Url.Link, nameof(BestRank), page, pagesize,100),
                data = results
            };

            return Ok(returnobj);
        }

        [HttpPost("exact")]
        public IActionResult Exact(Search search, int page = 0, int pagesize = 10)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var results = _dataservice.SearchExact(search.SearchString, int.Parse(userid), page, pagesize);

            return Ok(results);
        }

        [HttpPost("besttfidf")]
        public IActionResult BestTFIDF(Search search, int page = 0, int pagesize = 10)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var results = _dataservice.SearchBestTFIDF(search.SearchString, int.Parse(userid), page, pagesize);

            return Ok(results);
        }


        [HttpPost("cloud/simple")]
        public IActionResult CloudSimple(Search search)
        {

            var results = _dataservice.WordCloudSimple(search.SearchString);

            return Ok(results);
        }



        [HttpPost("cloud/tfidf")]
        public IActionResult CloudTFIDF(Search search)
        {
            var results = _dataservice.WordCloudTFIDF(search.SearchString);

            return Ok(results);
        }

        [HttpGet("assoc/{term}")]
        public IActionResult Testassoc(string term)
        {
            var results = _dataservice.WordAssociationSearch(term);

            return Ok(results);
        }

        [HttpGet("force/{term}/{grade}")]
        public IActionResult TestForce(string term, int grade)
        {
            var results = _dataservice.ForceGraph(term, grade);
            var data = "";
            foreach (var node in results)
            {
                data += node.Node;
            }
            return Ok(data);
        }

    }
}
