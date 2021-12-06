using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class CreateSessionDto
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int TheaterId { get; set; }

        public DateTime ClosingTime { get; set; }
    }
}
