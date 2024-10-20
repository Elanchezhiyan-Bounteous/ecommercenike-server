using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommercenike_server.Models;

namespace ecommercenike_server.Services
{
    public class CartService : ICartService
    {
        private readonly Supabase.Client _client;
        private readonly ProductService _productService;

        public CartService(Supabase.Client client, ProductService productService)
        {
            _client = client;
            _productService = productService;
        }

        public async Task<Cart> AddProductToCart(Guid userId, Guid productId, int quantity)
        {
            var response = await _client.From<Cart>()
                                        .Where(c => c.UserId == userId)
                                        .Get();

            var cart = response.Models.FirstOrDefault();

            if (cart == null)
            {
                // Create a new cart if it doesn't exist
                cart = new Cart
                {
                    UserId = userId,
                    Products = new Dictionary<Guid, int>
                    {
                        { productId, quantity }
                    },
                };
                await _client.From<Cart>().Insert(cart);
            }
            else
            {
                // If the product exists, update the quantity
                if (cart.Products.ContainsKey(productId))
                    cart.Products[productId] += quantity;
                else
                    cart.Products.Add(productId, quantity);

                await _client.From<Cart>().Update(cart);
            }

            return cart;
        }

        public async Task<CartWithProductDetails> GetAllProductsFromCart(Guid userId)
        {

            var response = await _client.From<Cart>()
                                        .Where(c => c.UserId == userId)
                                        .Get();

            var cartList = response.Models;


            if (cartList == null || !cartList.Any())
                return new CartWithProductDetails();

            var result = new CartWithProductDetails();

            foreach (var cart in cartList)
            {
                var productDetails = new List<ProductDetail>();

                foreach (var productId in cart.Products.Keys)
                {
                    var product = await _productService.GetProductById(productId);

                    if (product != null)
                    {
                        productDetails.Add(new ProductDetail
                        {
                            ProductId = product.Id,
                            Name = product.Name,
                            Price = product.Price,
                            Quantity = cart.Products[productId]
                        });
                    }
                }

                result = new CartWithProductDetails
                {
                    CartId = cart.Id,
                    UserId = cart.UserId,
                    Products = productDetails
                };
            }

            return result;
        }

        public async Task RemoveProductFromCart(Guid userId, Guid productId)
        {
            var response = await _client.From<Cart>()
                                        .Where(c => c.UserId == userId)
                                        .Get();

            var cart = response.Models.FirstOrDefault();

            if (cart != null && cart.Products.ContainsKey(productId))
            {
                cart.Products.Remove(productId);
                await _client.From<Cart>().Update(cart);
            }
        }
    }
}

public class ProductDetail
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class CartWithProductDetails
{
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }
    public List<ProductDetail> Products { get; set; }
}