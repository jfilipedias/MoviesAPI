using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos.Theater
{
    public class CreateTheaterDto
    {
        [Required(ErrorMessage = "The name field are required")]
        public string Name { get; set; }

        public int AddressFk { get; set; }

        public int ManagerFk { get; set; }
    }
}
