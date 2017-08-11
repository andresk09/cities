using AutoMapper;
using OpLab.API.Application.Dtos;
using OpLab.API.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using OpLab.API.Domain.Entities;

namespace OpLab.API.Application
{
    public class PointOfInterestService : IPointOfInterestService
    {
        private readonly IMapper _mapper;
        private readonly IPointOfInterestRepository _repository;

        public PointOfInterestService(IMapper mapper, IPointOfInterestRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Create(PointOfInterestDto input)
        {
            var pointOfInterest = _mapper.Map<PointOfInterest>(input);
            await _repository.Add(pointOfInterest);
        }

        public async Task<PointOfInterestDto> Get(int cityId, int id)
        {
            var pointOfInterest = await _repository.Get(cityId, id);
            var dto = _mapper.Map<PointOfInterestDto>(pointOfInterest);
            return dto;
        }

        public async Task<List<PointOfInterestDto>> GetForCity(int cityId)
        {
            var pointsOfInterest = await _repository.GetForCity(cityId);
            var dtos = _mapper.Map<List<PointOfInterestDto>>(pointsOfInterest);
            return dtos;
        }

        public async Task Remove(PointOfInterestDto input)
        {
            await _repository.Remove(input.Id);
        }

        public async Task Update(PointOfInterestDto input)
        {
            var pointsOfInterest = _mapper.Map<PointOfInterest>(input);
            await _repository.Update(pointsOfInterest);
        }
    }
}
