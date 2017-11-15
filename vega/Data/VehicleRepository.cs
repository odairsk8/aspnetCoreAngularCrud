using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Vega.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using Vega.Core;
using vega.Core.Entities;

namespace vega.Core
{
    public class VehicleRepository : IVehicleRepository
    {
        private VegaDbContext _context;

        public VehicleRepository(VegaDbContext context)
        {
            this._context = context;
        }

        public async Task<QueryResult<Vehicle>> GetAll(VehicleQuery queryObj)
        {
            var queryResult = new QueryResult<Vehicle>();
            var query = this._context.Vehicle
                 .Include(v => v.Features)
                     .ThenInclude(vf => vf.Feature)
                 .Include(v => v.Model)
                     .ThenInclude(m => m.Make)
                .AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(q => q.Model.MakeId == queryObj.MakeId.Value);

            if (queryObj.ModelId.HasValue)
                query = query.Where(q => q.Model.MakeId == queryObj.ModelId.Value);

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            queryResult.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);

            queryResult.Items = await query.ToListAsync();
            return queryResult;
        }

       

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await this._context.Vehicle.FindAsync(id);

            return await this._context.Vehicle
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(i => i.Id == id);


        }

        public void Add(Vehicle vehicle)
        {
            this._context.Vehicle.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            this._context.Remove(vehicle);
        }


    }
}
