using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class UpdateManagerDto
    {
        [Required]
        public string Name { get; set; }
    }
}
