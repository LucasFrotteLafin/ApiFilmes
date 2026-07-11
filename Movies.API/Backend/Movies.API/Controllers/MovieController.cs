using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Requests.Movies;

namespace Movies.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MovieController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(MovieCreateRequest request)
    {
        var movieService = new Services.MovieService();
        var isCreated = movieService.Create(request);
        if (!isCreated)
            return BadRequest("Failed to create movie.");
        return Ok("Movie created successfully.");
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var movieService = new Services.MovieService();
        var movie = movieService.GetById(id);
        if (movie == null)
            return NotFound("Movie not found.");
        return Ok(movie);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, MovieUpdateRequest request)
    {
        var movieService = new Services.MovieService();
        var isUpdated = movieService.Update(id, request);
        if (!isUpdated)
            return BadRequest("Failed to update movie.");
        return Ok("Movie updated successfully.");
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var movieService = new Services.MovieService();
        var isDeleted = movieService.Delete(id);
        if (!isDeleted)
            return BadRequest("Failed to delete movie.");
        return Ok("Movie deleted successfully.");
    }

    [HttpGet("get-all")]

    public IActionResult Get()
    {
        var movieService = new Services.MovieService();
        var movies = movieService.GetAll();
        return Ok(movies);
    }  
}
