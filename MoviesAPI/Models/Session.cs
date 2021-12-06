using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoviesAPI.Models
{
    public class Session
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public DateTime ClosingTime { get; set; }

        [JsonIgnore]
        public virtual Movie Movie { get; set; }

        public int MovieId { get; set; }

        [JsonIgnore]
        public virtual Theater Theater { get; set; }

        public int TheaterId { get; set; }
    }
}
