using OpLab.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpLab.API.Domain.Repositories
{
    public interface IPointOfInterestRepository
    {
        Task<PointOfInterest> Get(int cityId, int id);

        Task<IEnumerable<PointOfInterest>> GetForCity(int cityId);

        Task Add(PointOfInterest entity);

        Task Update(PointOfInterest entity);

        Task Remove(int id);
    }
}
