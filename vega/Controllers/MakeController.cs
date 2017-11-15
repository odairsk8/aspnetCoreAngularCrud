using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using vega;
using vega.Resources;
using Vega.Core.Entities;

namespace Vega.Web.Api.Controllers
{

    public class MakeController : Controller
    {
        private readonly VegaDbContext _context;
        public IMapper _mapper { get; }

        public MakeController(VegaDbContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }


        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetAll()
        {
            var makes = await this._context.Make.Include(m => m.Models).ToListAsync();

            return this._mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

    }
}