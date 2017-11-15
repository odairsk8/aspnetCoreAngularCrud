using Vega.Core;

namespace vega.Resources
{
    public class VehicleQueryResource : IQueryObject
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string SortBy { get; set; }
        public bool isSortAscending { get; set; }

        public int Page { get; set; }  
        public int PageSize { get; set; }
    }
}