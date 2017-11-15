using System;
using System.Collections.Generic;
using System.Text;

namespace vega.Core.Entities
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
