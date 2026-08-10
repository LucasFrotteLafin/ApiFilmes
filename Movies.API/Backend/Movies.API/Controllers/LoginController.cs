using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.API.Authentication;
using Movies.API.DatabaseContext;
using Movies.API.Encrypt;
using Movies.API.Requests.Login;
namespace Movies.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody] AuthRequest request)
    {
        using var connection = new DataContext();
        var user = connection.Users.AsNoTracking().FirstOrDefault(x => x.Username == request.Username && x.Password == PasswordEncryptor.EncryptPassword(request.Password));
        if (user is null)
            return BadRequest("Login failed!");
        var token = JwtAuthManager.GenerateToken(user.Username, user.Role, user.Id);
        return Ok(new { token });
    }
}