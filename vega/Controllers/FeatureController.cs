using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega;
using vega.Resources;

namespace Vega.Web.Api.Controllers
{
    public class FeatureController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public FeatureController(VegaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("api/features")]
        public async Task<IList<KeyValuePairResource>> GetAll() {
            var features = await this.context.Feature.ToListAsync();
            return this.mapper.Map<List<KeyValuePairResource>>(features);
        }
    }
}