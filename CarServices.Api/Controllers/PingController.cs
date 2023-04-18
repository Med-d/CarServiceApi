using CarServices.Api.Core.AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Controllers;

[ApiController]
public class PingController : Controller
{
    private readonly IdentityService identityService;

    public PingController(IdentityService identityService)
    {
        this.identityService = identityService;
    }

    [HttpGet("ping")]
    public IActionResult Ping() => Ok("pong");
}