using System.Security.Cryptography;
using System.Text;
using CarServices.Api.Core.AuthServices;
using CarServices.Api.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly IdentityService identityService;
    private readonly CryptoHelper cryptoHelper;

    public AuthController(IdentityService identityService, CryptoHelper cryptoHelper)
    {
        this.identityService = identityService;
        this.cryptoHelper = cryptoHelper;
    }

    [HttpPost("authorize")]
    public async Task<IActionResult> Authorize(AuthRequest request)
    {
        var token = cryptoHelper.GetToken(request.Login, request.Password);
        var identityResult = await identityService.GetIdentity(token);

        if (identityResult.HasError())
            return Unauthorized();
        var identity = identityResult.ValueOrThrow();

        return new OkObjectResult(identity);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {   // todo: Регать может только админ
        var token = cryptoHelper.GetToken(request.Login, request.Password);
        var isRegisterd = await identityService.RegisterIdentity(
            request.Name,
            request.Surname,
            request.Login,
            token,
            request.GetRole()
        );

        if (!isRegisterd)
            return BadRequest();

        var identity = await identityService.GetIdentity(token);
        var result = identity.ValueOrThrow();
        
        return new OkObjectResult(result);
    }
    
}