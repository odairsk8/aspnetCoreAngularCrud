using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Core.Entities;

namespace Vega.Core.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}