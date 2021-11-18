using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos.Theater
{
    public class UpdateTheaterDto
    {
        [Required(ErrorMessage = "The name field are required")]
        public string Name { get; set; }
    }
}
