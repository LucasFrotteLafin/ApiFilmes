namespace Movies.API.Models;
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public string Overview { get; set; } = null!;
    public string Genre { get; set; } = string.Empty;
    public double Rating { get; set; } = 0;
    public string TrailerUrl { get; set; } = string.Empty;
    public Movie() { }
    public Movie(string title, string posterUrl, string overview, string genre, double rating, string trailerUrl)
    {
        Title = title;
        PosterUrl = posterUrl;
        Overview = overview;
        Genre = genre;
        Rating = rating;
        TrailerUrl = trailerUrl;
    }
}