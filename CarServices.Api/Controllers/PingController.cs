using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Controllers;

[ApiController]
public class PingController : Controller
{
    [HttpGet("ping")]
    public IActionResult Ping() => Ok("pong");
}