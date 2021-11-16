using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Movie
    {
        [Required(ErrorMessage = "The title field is required.")]
        public string Title { get; set; }

        [Required]
        public string Director { get; set; }

        [StringLength (100, ErrorMessage = "The Genre field must be at most 100 characters.")]
        public string Genre { get; set; }

        [Range(1, 600, ErrorMessage = "The Duration field must be at least 1 minute and at most 600 minutes")]
        public int Duration {  get; set; }
    }
}
