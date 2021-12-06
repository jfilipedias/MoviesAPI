using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class CreateSessionDto
    {
        [Required]
        public int MovieId;

        [Required]
        public int TheaterId;

        public DateTime ClosingTime;
    }
}
