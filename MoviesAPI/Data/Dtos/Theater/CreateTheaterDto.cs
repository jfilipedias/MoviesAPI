using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class CreateTheaterDto
    {
        [Required(ErrorMessage = "The name field are required")]
        public string Name { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        public int ManagerId { get; set; }                                      
    }
}
