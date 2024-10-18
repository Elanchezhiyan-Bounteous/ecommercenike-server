using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommercenike_server.Models;
using ecommercenike_server.Contracts;
using Supabase.Postgrest.Responses;

namespace ecommercenike_server.Services
{
    public class FilterService : IFilterService
    {
        private readonly Supabase.Client _client;

        public FilterService(Supabase.Client client)
        {
            _client = client;
        }

        public async Task<List<Product>> GetFilteredProducts(FilterRequest filters)
        {
            var query = new List<Product>();

            // Apply Gender filter
            // if (filters.Gender != null && filters.Gender.Length > 0)
            // {
            //     foreach (var gender in filters.Gender)
            //     {
            //         var genderFiltered = await _client.From<Product>().Where(p => p.Gender == gender).Get();
            //         query.AddRange(genderFiltered.Models);  // Add gender-filtered products to the query list
            //     }
            // }

            // Apply Price range filter
            if (filters.PriceRange != null && filters.PriceRange.Length > 0)
            {
                foreach (var range in filters.PriceRange)
                {
                    var priceFiltered = range switch
                    {
                        "under2500" => await _client.From<Product>().Where(p => p.Price < 2500).Get(),
                        "2501to7500" => await _client.From<Product>().Where(p => p.Price >= 2501 && p.Price <= 7500).Get(),
                        "7501to12500" => await _client.From<Product>().Where(p => p.Price >= 7501 && p.Price <= 12500).Get(),
                        "over13000" => await _client.From<Product>().Where(p => p.Price > 13000).Get(),
                        _ => null
                    };

                    if (priceFiltered != null)
                    {
                        query.AddRange(priceFiltered.Models);  
                    }
                }
            }

            if (filters.SaleOffers != null && filters.SaleOffers.Length > 0)
            {
                foreach (var offer in filters.SaleOffers)
                {
                    var offerFiltered = offer switch
                    {
                        "Sale" => await _client.From<Product>().Where(p => p.SpecialMention == "Sale").Get(),
                        "Best Seller" => await _client.From<Product>().Where(p => p.SpecialMention == "Best Seller").Get(),
                        "Just In" => await _client.From<Product>().Where(p => p.SpecialMention == "Just In").Get(),
                        _ => null
                    };

                    if (offerFiltered != null)
                    {
                        query.AddRange(offerFiltered.Models);
                    }
                }
            }

            if (filters.Brand != null && filters.Brand.Length > 0)
            {
                foreach (var brand in filters.Brand)
                {
                    var brandFiltered = await _client.From<Product>().Where(p => p.Category == brand).Get();
                    query.AddRange(brandFiltered.Models); 
                }
            }

            var distinctProducts = query.GroupBy(p => p.Id).Select(g => g.First()).ToList();

            
            return distinctProducts;
        }



    }

}
