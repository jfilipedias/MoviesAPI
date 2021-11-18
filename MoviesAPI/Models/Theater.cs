using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Theater
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name field are required")]
        public string Name { get; set; }
    }
}
