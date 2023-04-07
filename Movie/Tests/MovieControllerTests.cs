using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MoviesApi.Controllers;
using MoviesApi.Dtos;
using MoviesApi.Models;
using Xunit;

namespace MoviesApi.Tests
{
    public class MoviesControllerTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetMovies_ReturnsOkObjectResult_WithMovieDtos()
        {
            // Arrange
            var dbContextMock = new Mock<MovieContext>(new DbContextOptionsBuilder<MovieContext>().Options);
            var movies = new List<Movie>()
            {
                new Movie() { Id = 1, Title = "Movie 1", Genre = "Action", ReleaseDate = new DateTime(2020, 1, 1) },
                new Movie() { Id = 2, Title = "Movie 2", Genre = "Comedy", ReleaseDate = new DateTime(2020, 1, 2) }
            };
            dbContextMock.Setup(x => x.Movies).Returns(DbSetHelper.GetDbSetMock(movies).Object);

            var controller = new MoviesController(dbContextMock.Object);

            // Act
            var result = await controller.GetMovies();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var movieDtos = Assert.IsAssignableFrom<IEnumerable<MovieDto>>(okObjectResult.Value);
            Assert.Equal(2, movieDtos.Count());
            Assert.Equal("Movie 1", movieDtos.First().Title);
            Assert.Equal("Action", movieDtos.First().Genre);
            Assert.Equal(new DateTime(2020, 1, 1), movieDtos.First().ReleaseDate);
            Assert.Equal("Movie 2", movieDtos.Last().Title);
            Assert.Equal("Comedy", movieDtos.Last().Genre);
            Assert.Equal(new DateTime(2020, 1, 2), movieDtos.Last().ReleaseDate);

            // Verify
            dbContextMock.Verify();
        }
    }
}
