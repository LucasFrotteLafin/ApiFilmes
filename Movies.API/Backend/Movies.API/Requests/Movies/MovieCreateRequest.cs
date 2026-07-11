namespace Movies.API.Requests.Movies;

public class MovieCreateRequest
{
    public string Title { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public string Overview { get; set; } = null!;
}
