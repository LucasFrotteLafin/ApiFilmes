namespace Movies.API.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public string Overview { get; set; } = null!;
    public Movie()
    {

    }
    public Movie(string title, string posterUrl, string overview)
    {
        Title = title;
        PosterUrl = posterUrl;
        Overview = overview;
    }
}
