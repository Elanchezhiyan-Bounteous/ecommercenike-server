using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommercenike_server.Contracts;
using ecommercenike_server.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommercenike_server.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProductToCart(AddToCartRequest request)
        {
            var cart = await _cartService.AddProductToCart(request.UserId, request.ProductId, request.Quantity);
            return Ok(cart);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllProductsFromCart(Guid userId)
        {
            var products = await _cartService.GetAllProductsFromCart(userId);
            return Ok(products);
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveProductFromCart(Guid userId, Guid productId)
        {
            await _cartService.RemoveProductFromCart(userId, productId);
            return NoContent();
        }
    }
}