using System.ComponentModel.DataAnnotations;

namespace OpLab.API.Application.Dtos
{
    public class PointOfInterestDto
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        public int CityId { get; set; }
    }
}
