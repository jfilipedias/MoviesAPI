using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class UpdateTheaterDto
    {
        [Required(ErrorMessage = "The name field are required")]
        public string Name { get; set; }

        [Required]
        public int AddressId { get; set; }

        public int ManagerId { get; set; }
    }
}
