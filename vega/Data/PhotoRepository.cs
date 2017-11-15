using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core.Entities;
using Vega.Core.Interfaces;

namespace vega.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly VegaDbContext context;

        public PhotoRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await this.context.Photo
                            .Where(p => p.VehicleId == vehicleId)
                            .ToListAsync();
        }


    }
}