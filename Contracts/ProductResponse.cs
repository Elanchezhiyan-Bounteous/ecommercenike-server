using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommercenike_server.Models;

namespace ecommercenike_server.Contracts
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Src { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        public string SpecialMention { get; set; }
        public List<Review> Reviews { get; set; }
        public float Rating { get; set; }
        public List<ProductSize> Sizes { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProductColor> Colors { get; set; }
        public List<Image> ProductGallery { get; set; }
        public List<Image> DescriptionImages { get; set; }
    }
}