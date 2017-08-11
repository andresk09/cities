using AutoMapper;
using OpLab.API.Application.Dtos;
using OpLab.API.Domain.Entities;
using System.Collections.Generic;

namespace OpLab.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<Data.Models.City, City>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();

            CreateMap<City, GetCityOutput>()
                    .ForMember(d => d.Cities, o => o.MapFrom(s => new List<CityDto>()))
                    .AfterMap((s, d) => d.Cities.Add(Mapper.Map<City, CityDto>(s)));



            CreateMap<PointOfInterestDto, PointOfInterest>().ReverseMap();
            CreateMap<Data.Models.PointOfInterest, PointOfInterest>().ReverseMap();
            CreateMap<PointOfInterest, PointOfInterestDto>().ReverseMap();


        }
    }
}
