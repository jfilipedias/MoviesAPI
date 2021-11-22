using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    [Table("Managers")]
    public class Manager
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Theater> Theaters { get; set; }
    }
}
