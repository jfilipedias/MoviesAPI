using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class CreateManagerDto
    {
        [Required]
        public string Name { get; set; }
    }
}
