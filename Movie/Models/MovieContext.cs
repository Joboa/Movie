﻿using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = null;
    }
}
