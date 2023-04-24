using CarServices.Api.Models.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Models.Requests.Event;

public record CloseOrderRequset(
    [FromBody] CloseOrder Data,
    [FromBody] string Channel,
    [FromBody] Guid ParrentMessageId
)
{
    
}