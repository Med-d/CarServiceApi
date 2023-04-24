using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Models.Requests;

public record AuthRequest(
    [FromBody] string Login,
    [FromBody] string Password
)
{
}