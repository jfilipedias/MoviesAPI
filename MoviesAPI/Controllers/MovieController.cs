using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private static int id = 0;
        private static List<Movie> movies = new List<Movie>();

        [HttpPost(Name = "PostMovie")]
        public void AddMovie([FromBody] Movie movie)
        {
            movie.Id = id++;
            movies.Add(movie);
        }

        [HttpGet(Name = "GetMovie")]
        public IEnumerable<Movie> GetAllMovies()
        {
            return movies;
        }
    }
}
