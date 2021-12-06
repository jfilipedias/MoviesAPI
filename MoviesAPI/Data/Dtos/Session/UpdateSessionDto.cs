using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class UpdateSessionDto
    {
        [Required]
        public int MovieId;

        [Required]
        public int TheaterId;

        public DateTime ClosingTime;
    }
}
