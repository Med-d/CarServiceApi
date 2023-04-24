using System.Text.Json;
using CarServices.Api.Core.AuthServices;
using CarServices.Api.Core.NotificationsServices;
using CarServices.Api.Core.UnitOfWork.Models;
using CarServices.Api.Models.Requests.Event;
using CarServices.Api.Models.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Controllers;

[ApiController]
[Route("api/event")]
public class EventController : Controller
{
    private readonly IdentityService identityService;
    private readonly EventBus eventBus;

    public EventController(
        IdentityService identityService,
        EventBus eventBus
    )
    {
        this.identityService = identityService;
        this.eventBus = eventBus;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Notification>> CreateEvent([FromBody] CreateEventRequset requset)
    {
        var token = Token.FromHeaders(HttpContext.Request.Headers);
        var identity = (await identityService.GetIdentity(token)).ValueOrThrow();
        Console.WriteLine($"{identity.Login}");
        if (!(identity.Role >= Role.Manager))
            return Forbid();

        var notification = await eventBus.Publish(requset.Channel, JsonSerializer.Serialize(requset.Data), identity.Id);
        
        return Created($"api/order/{notification.OrderId}/listen", notification);

    }


    [HttpGet("subscribed")]
    public async Task<ActionResult<IEnumerable<Guid>>> GetEvents()
    {
        var token = Token.FromHeaders(HttpContext.Request.Headers);
        var identity = (await identityService.GetIdentity(token)).ValueOrThrow();

        var events = await eventBus.ListenSubscribes(identity.Id);

        return Ok(events);
    }
}