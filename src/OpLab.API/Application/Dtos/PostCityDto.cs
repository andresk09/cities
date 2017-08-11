using System.ComponentModel.DataAnnotations;

namespace OpLab.API.Application.Dtos
{
    public class PostCityDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }
    }
}
