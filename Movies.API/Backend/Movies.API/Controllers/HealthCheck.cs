using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers;

[ApiController]
[Route("api/[controller]")]
    public class HeatlCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Check()
        {
            return Ok("The API is working");
        }
    }
