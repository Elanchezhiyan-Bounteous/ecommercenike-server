using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommercenike_server.Contracts;
using ecommercenike_server.Models;

namespace ecommercenike_server.Services
{
      public interface IProductService
    {
        Task<Product> CreateProduct(CreateProductRequest request);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid id); 
        Task<List<Product>> GetProductsByCategory(string category);
        Task DeleteProduct(Guid id);  
    }

}