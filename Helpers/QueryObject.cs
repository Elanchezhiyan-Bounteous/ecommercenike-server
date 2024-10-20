using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommercenike_server.Helpers
{
    public class QueryObject
    {
        public string? SortBy { get; set; } = null;

        public bool IsDescending { get; set; } = false;
    }
}