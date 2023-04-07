namespace MoviesApi.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
