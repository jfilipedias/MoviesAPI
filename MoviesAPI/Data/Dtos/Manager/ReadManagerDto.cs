using MoviesAPI.Models;

namespace MoviesAPI.Data.Dtos
{
    public class ReadManagerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Theater> Theaters {  get; set; }
    }
}
