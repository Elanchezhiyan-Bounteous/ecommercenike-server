using System;

namespace ecommercenike_server.Contracts
{
    public class AddToCartRequest
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; } 
        public int Quantity { get; set; } 
    }
}
