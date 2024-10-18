using ecommercenike_server.Models;
using ecommercenike_server.Contracts;
using System.Reflection.Metadata;

namespace ecommercenike_server.Services
{
    public class ProductService : IProductService
    {
        private readonly Supabase.Client _client;
        private readonly FilterService _filterService;


        public ProductService(Supabase.Client client, FilterService filterService)
        {
            _client = client;
            _filterService = filterService;
        }

        public async Task<Product> CreateProduct(CreateProductRequest request)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Desc = request.Desc,
                Category = request.Category,
                Price = request.Price,
                Src = request.Src,
                OriginalPrice = request.OriginalPrice,
                SpecialMention = request.SpecialMention,
                Reviews = request.Reviews.Select(rev => new Review
                {
                    Name = rev.Name,
                    Feedback = rev.Feedback,
                }).ToList(),
                Rating = request.Rating,
                Sizes = request.Sizes,

                ProductGallery = request.ProductGallery.Select(gal => new Image
                {
                    Alt = gal.Alt,
                    ImageUrl = gal.ImageUrl,
                }).ToList(),
                DescriptionImages = request.DescriptionImages.Select(x => new Image
                {
                    Alt = x.Alt,
                    ImageUrl = x.ImageUrl,
                }).ToList(),
            };

            var response = await _client.From<Product>().Insert(product);
            return response.Models.First();
        }

        public async Task<List<Product>> GetAllProducts(FilterRequest filters)
        {

            if ((filters.Brand == null || filters.Brand.Length == 0) &&
                (filters.SaleOffers == null || filters.SaleOffers.Length == 0) &&
                (filters.PriceRange == null || filters.PriceRange.Length == 0) &&
                (filters.Gender == null || filters.Gender.Length == 0))
            {
                var response = await _client.From<Product>().Get();
                return response.Models;
            }

            var filteredresponse = await _filterService.GetFilteredProducts(filters);

            return filteredresponse;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var response = await _client.From<Product>()
                .Where(n => n.Id == id)
                .Get();

            return response.Models.FirstOrDefault();
        }

        public async Task<List<Product>> GetProductsByCategory(string category)
        {
            var response = await _client.From<Product>()
                .Where(n => n.Category == category)
                .Get();

            return response.Models;
        }

        public async Task DeleteProduct(Guid id)  // Change from long to Guid
        {
            await _client.From<Product>()
                .Where(n => n.Id == id)
                .Delete();
        }
    }
}
