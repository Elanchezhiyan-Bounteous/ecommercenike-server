using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommercenike_server.Contracts;
using ecommercenike_server.Models;
using Supabase.Postgrest.Responses;

namespace ecommercenike_server.Services
{
     public interface IFilterService
    {
        Task<List<Product>> GetFilteredProducts(FilterRequest request);
    }
}