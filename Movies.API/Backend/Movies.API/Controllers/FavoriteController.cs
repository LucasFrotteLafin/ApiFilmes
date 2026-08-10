using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.API.DatabaseContext;
using Movies.API.Models;
using System.Security.Claims;
namespace Movies.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FavoriteController : ControllerBase
{
    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
    [HttpGet]
    public IActionResult GetAll()
    {
        using var db = new DataContext();
        var userId = GetUserId();
        var favorites = db.Favorites
            .Where(f => f.UserId == userId)
            .Select(f => f.Movie)
            .ToList();
        return Ok(favorites);
    }
    [HttpPost("{movieId:int}")]
    public IActionResult Add(int movieId)
    {
        using var db = new DataContext();
        var userId = GetUserId();
        if (db.Favorites.Any(f => f.UserId == userId && f.MovieId == movieId))
            return Conflict("Already favorited.");
        db.Favorites.Add(new Favorite { UserId = userId, MovieId = movieId });
        db.SaveChanges();
        return Ok();
    }
    [HttpDelete("{movieId:int}")]
    public IActionResult Remove(int movieId)
    {
        using var db = new DataContext();
        var userId = GetUserId();
        var fav = db.Favorites.FirstOrDefault(f => f.UserId == userId && f.MovieId == movieId);
        if (fav == null) return NotFound();
        db.Favorites.Remove(fav);
        db.SaveChanges();
        return Ok();
    }
}