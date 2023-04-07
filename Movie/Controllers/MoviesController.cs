using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Dtos;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _dbContext;

        public MoviesController(MovieContext dbContext) 
        {
            _dbContext = dbContext;
        }

        // GET: api/Movies
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (_dbContext.Movies == null)
            {
                return NotFound();
            }
            return await _dbContext.Movies.ToListAsync();
        }
        */

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _dbContext.Movies.ToListAsync();
            var movieDtos = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                ReleaseDate = m.ReleaseDate
            });
            return Ok(movieDtos);
        }

        // GET: api/Movies/id
        /*
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetSingleMovie(int id)
        {
            if (_dbContext.Movies == null)
            {
                return NotFound();
            }
            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null) 
            {
                return NotFound();
            }
            return movie;
        }
        */

        // GET: api/Movies/id
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetSingleMovie(int id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate
            };

            return Ok(movieDto);
        }

        // POST: api/Movies
        /*
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSingleMovie), new {id = movie.Id}, movie);
        }
        */
        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieDto movieDto)
        {
            var movie = new Movie
            {
                Title = movieDto.Title,
                Genre = movieDto.Genre,
                ReleaseDate = movieDto.ReleaseDate
            };

            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();

            movieDto.Id = movie.Id;

            return CreatedAtAction(nameof(GetSingleMovie), new { id = movieDto.Id }, movieDto);
        }

        // PUT: api/Movies/id
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, [FromBody] Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest("The movie ID in the URL does not match the movie ID in the request body.");
            }

            //ar existingMovie = await _dbContext.Movies.FindAsync(id);
            //if (existingMovie == null)
            //{
            //    return NotFound("The movie with the given ID was not found.");
            //}

            //existingMovie.Title = movie.Title;
            //existingMovie.Genre = movie.Genre;
            //existingMovie.ReleaseDate = movie.ReleaseDate;

            _dbContext.Entry(movie).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound("The movie with the given ID was not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        */
        // PUT: api/Movies/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, [FromBody] MovieDto movieDto)
        {
            if (id != movieDto.Id)
            {
                return BadRequest("The movie ID in the URL does not match the movie ID in the request body.");
            }

            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound("The movie with the given ID was not found.");
            }

            movie.Title = movieDto.Title;
            movie.Genre = movieDto.Genre;
            movie.ReleaseDate = movieDto.ReleaseDate;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Movie/id
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_dbContext.Movies == null)
            {
                return NotFound();
            }

            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null) 
            {
                return NotFound();
            }

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();

            return NoContent();
            //return Ok(movie);
        }
        */

        // DELETE: api/Movie/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieDto>> DeleteMovie(int id)
        {
            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();

            //var movieDto = new MovieDto { Id = movie.Id };
            return NoContent();
        }

        /*
        private bool MovieExists(long id)
        {
            return _dbContext.Movies.Any(e => e.Id == id);
        }
        */


    }
}
