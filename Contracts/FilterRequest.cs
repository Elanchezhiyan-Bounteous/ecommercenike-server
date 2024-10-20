using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommercenike_server.Contracts
{
    public class FilterRequest
    {
        public string[]? Gender { get; set; }
        public string[]? PriceRange { get; set; }
        public string[]? SaleOffers { get; set; }
        public string[]? Brand { get; set; }


    }
}