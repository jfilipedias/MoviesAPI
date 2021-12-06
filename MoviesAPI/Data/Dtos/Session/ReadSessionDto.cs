﻿using MoviesAPI.Models;

namespace MoviesAPI.Data.Dtos
{
    public class ReadSessionDto
    {
        public int Id { get; set; } 

        public Movie Movie { get; set; }

        public Theater Theater { get; set; }
    }
}
