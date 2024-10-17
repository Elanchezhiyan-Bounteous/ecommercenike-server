using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommercenike_server.Models;

namespace ecommercenike_server.Services
{
        public interface ICartService
    {
        Task<Cart> AddProductToCart(Guid userId, Guid productId, int quantity);
        Task<IEnumerable<CartWithProductDetails>> GetAllProductsFromCart(Guid userId);
        Task RemoveProductFromCart(Guid userId, Guid productId);
    }
}