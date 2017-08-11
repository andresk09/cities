using System.Collections.Generic;

namespace OpLab.API.Application.Dtos
{
    public class GetCityOutput
    {
        public IList<CityDto> Cities { get; set; }
    }
}
