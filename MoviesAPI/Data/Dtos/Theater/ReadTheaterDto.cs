using System.ComponentModel.DataAnnotations;
using MoviesAPI.Models;

namespace MoviesAPI.Data.Dtos
{
    public class ReadTheaterDto
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public Manager Manager { get; set; }
    }
}
