using System.Threading.Tasks;
using Vega.Core.Entities;
using System.Collections.Generic;
using vega.Core.Entities;

namespace vega.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        Task<QueryResult<Vehicle>> GetAll(VehicleQuery filter);
    }
}