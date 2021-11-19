using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos.Theater
{
    public class CreateTheaterDto
    {
        [Required(ErrorMessage = "The name field are required")]
        public string Name { get; set; }

        public int AddressId { get; set; }

        public int ManagerId { get; set; }
    }
}
