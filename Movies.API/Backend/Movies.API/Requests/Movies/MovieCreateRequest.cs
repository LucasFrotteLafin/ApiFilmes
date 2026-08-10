namespace Movies.API.Requests.Movies;
public class MovieCreateRequest
{
    public string Title { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public string Overview { get; set; } = null!;
    public string Genre { get; set; } = string.Empty;
    public double Rating { get; set; } = 0;
    public string TrailerUrl { get; set; } = string.Empty;
}