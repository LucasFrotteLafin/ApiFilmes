using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Requests.Movies;
using Movies.API.Requests.Users;

namespace Movies.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Create(UserCreateRequest request)
    {
        var userService = new Services.UserService();
        var isCreated = userService.Create(request);
        if (!isCreated)
            return BadRequest("Failed to create user.");
        return Ok("User created successfully.");
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var userService = new Services.UserService();
        var user = userService.GetById(id);
        if (user == null)
            return NotFound("User not found.");
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UserUpdateRequest request)
    {
        var userService = new Services.UserService();
        var isUpdated = userService.Update(id, request);
        if (!isUpdated)
            return BadRequest("Failed to update user.");
        return Ok("User  updated successfully.");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var userService = new Services.UserService();
        var isDeleted = userService.Delete(id);
        if (!isDeleted)
            return BadRequest("Failed to delete user.");
        return Ok("User deleted successfully.");
    }

    [HttpGet("get-all")]
    public IActionResult Get()
    {
        var userService = new Services.UserService();
        var user = userService.GetAll();
        return Ok(user);
    }
}
