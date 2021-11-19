using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos.Theater
{
    public class ReadTheaterDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name field are required")]
        public string Name { get; set; }

        public int AddressFk { get; set; }
    }
}
