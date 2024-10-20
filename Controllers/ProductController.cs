using ecommercenike_server.Contracts;
using ecommercenike_server.Models;
using ecommercenike_server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace furniro_server.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            var product = await _productService.CreateProduct(request);
            return Ok(JsonConvert.SerializeObject(product));
        }

        [AllowAnonymous]
        [HttpPost("filter")]
        public async Task<IActionResult> GetAllProducts([FromBody] FilterRequest filters, [FromQuery] QueryRequest query)
        {
            var products = await _productService.GetAllProducts(filters, query);

            if (!products.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var products = await _productService.GetProductsByCategory(category);

            if (products is null || !products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductById(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
