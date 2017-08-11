using OpLab.API.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpLab.API.Application
{
    public interface IPointOfInterestService
    {
        Task<List<PointOfInterestDto>> GetForCity(int cityId);

        Task<PointOfInterestDto> Get(int cityId, int id);

        Task Create(PointOfInterestDto input);

        Task Update(PointOfInterestDto input);

        Task Remove(PointOfInterestDto input);
    }
}
