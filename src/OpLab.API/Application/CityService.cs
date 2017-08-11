using System.Threading.Tasks;
using OpLab.API.Application.Dtos;
using AutoMapper;
using OpLab.API.Domain.Repositories;
using OpLab.API.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OpLab.API.Application
{
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        private readonly IPointOfInterestRepository _poiRepository;

        public CityService(IMapper mapper, ICityRepository repository, IPointOfInterestRepository poiRepository)
        {
            _cityRepository = repository;
            _mapper = mapper;
            _poiRepository = poiRepository;
        }

        public async Task<bool> CityExists(int cityId)
        {
            var city = await _cityRepository.Get(cityId);
            return city != null;
        }        

        public async Task Create(CityDto input)
        {
            var city = _mapper.Map<City>(input);
            await _cityRepository.Add(city);
        }

        public async Task<List<CityDto>> Get()
        {
            var cities = await _cityRepository.Get();
            var dtos = _mapper.Map<List<CityDto>>(cities);
            return dtos;
        }

        public async Task<GetCityOutput> Get(GetCityInput input)
        {
            var city = await _cityRepository.Get(input.Id);
            var pointsOfInterest = await _poiRepository.GetForCity(input.Id);
            city.PointsOfInterest = pointsOfInterest.ToList();
            var dto = _mapper.Map<GetCityOutput>(city);

            return dto;
        }

        public async Task Remove(CityDto input)
        {
            await _cityRepository.Remove(input.Id);
        }

        public async Task Update(CityDto input)
        {
            var city = _mapper.Map<City>(input);
            await _cityRepository.Update(city);
        }        
    }
}
