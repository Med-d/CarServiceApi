using CarServices.Api.Core.UnitOfWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Models.Requests;

public record RegisterRequest(
    [FromBody] string Name,
    [FromBody] string Surname,
    [FromBody] string Login,
    [FromBody] string Password,
    [FromBody] string Role
)
{

    public Role GetRole()
    {
        if (!Enum.TryParse<Role>(Role, out var role))
            throw new InvalidDataException("Invalid role");
        return role;
    }
    
}