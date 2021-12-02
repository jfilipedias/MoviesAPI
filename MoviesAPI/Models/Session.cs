using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Session
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public DateTime ClosingTime { get; set; }

        public virtual Movie Movie { get; set; }

        public int MovieId { get; set; }

        public virtual Theater Theater { get; set; }

        public int TheaterId { get; set; }
    }
}
