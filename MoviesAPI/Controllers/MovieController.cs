﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            movie.Id = id++;
            movies.Add(movie);
            
            return CreatedAtAction(nameof(GetMovieById), new { Id = movie.Id }, movie);
        }

        [HttpGet(Name = "GetMovies")]
        public IActionResult GetAllMovies()
        {
            return Ok(movies);
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public IActionResult GetMovieById(int id)
        {
            var movie = movies.FirstOrDefault(movie => movie.Id == id);
            
            if (movie == null)
                return NotFound();
                
            return Ok(movie);
        }
    }
}
