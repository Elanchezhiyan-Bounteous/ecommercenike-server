using ecommercenike_server.Contracts;
using ecommercenike_server.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommercenike_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilterController : ControllerBase
    {
        private readonly IFilterService _filterService;

        public FilterController(IFilterService filterService)
        {
            _filterService = filterService;
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredProducts( FilterRequest request)
        {
            var filteredProducts = await _filterService.GetFilteredProducts(request);
            return Ok(filteredProducts);
        }
    }
}
