using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string PublicPlace { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public int Number { get; set; }

        public Theater Theater { get; set; }
    }
}
