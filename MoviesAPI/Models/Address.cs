using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string PublicPlace { get; set; }

        public string District { get; set; }

        public int Number { get; set; }

        public Theater Theater { get; set; }
    }
}
