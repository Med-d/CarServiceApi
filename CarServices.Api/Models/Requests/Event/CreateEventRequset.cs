using CarServices.Api.Models.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Models.Requests.Event;

public record CreateEventRequset(
    [FromBody] CreationOrder Data,
    [FromBody] string Channel
)
{
    
}