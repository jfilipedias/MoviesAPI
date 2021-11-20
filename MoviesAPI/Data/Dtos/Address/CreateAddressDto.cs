using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class CreateAddressDto
    {
        [Required]
        public string PublicPlace { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public int Number { get; set; }
    }
}
