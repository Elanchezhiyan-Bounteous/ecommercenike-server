using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommercenike_server.Contracts
{
    public class QueryRequest
    {
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}