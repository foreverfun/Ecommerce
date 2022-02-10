using Ecommerce.Api.Search.Models;
using Ecommerce.Api.Search.Services;
using Ecommerce.Api.Search.Services.OrderService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync()
        {
            return Ok("this is a get endpoint");
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await _searchService.SearchAsync(term.CustomerId); ;
            if (result.IsSuccess)
                return Ok(result.SearchResult);
            return NotFound();
        }
    }
}
