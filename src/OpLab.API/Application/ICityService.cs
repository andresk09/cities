using OpLab.API.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpLab.API.Application
{
    public interface ICityService
    {
        Task<List<CityDto>> Get();
        
        Task<GetCityOutput> Get(GetCityInput input);

        Task Create(CityDto input);

        Task Update(CityDto input);

        Task Remove(CityDto input);

        Task<bool> CityExists(int cityId);
    }
}