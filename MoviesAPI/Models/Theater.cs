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

        public virtual Address Address { get; set; }

        public int AddressId { get; set; }

        public int ManagerId { get; set; }
    }
}
