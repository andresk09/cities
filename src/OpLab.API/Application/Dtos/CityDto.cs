using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpLab.API.Application.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }      
        
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();
    }
}
