using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommercenike_server.Contracts;
using ecommercenike_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommercenike_server.Services
{
    public interface IProductService
    {
        Task<Product> CreateProduct(CreateProductRequest request);
        Task<List<Product>> GetAllProducts([FromBody] FilterRequest filters, [FromQuery] QueryRequest query);
        Task<Product> GetProductById(Guid id);
        Task<List<Product>> GetProductsByCategory(string category);
        Task DeleteProduct(Guid id);
    }

}