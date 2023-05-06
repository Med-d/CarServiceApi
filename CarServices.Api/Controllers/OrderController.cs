using System.Text.Json;
using CarServices.Api.Core.AuthServices;
using CarServices.Api.Core.NotificationsServices;
using CarServices.Api.Core.UnitOfWork.Models;
using CarServices.Api.Models.Requests.Event;
using CarServices.Api.Models.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : Controller
{
    private readonly IdentityService identityService;
    private readonly EventBus eventBus;

    public OrderController(
        IdentityService identityService,
        EventBus eventBus)
    {
        this.identityService = identityService;
        this.eventBus = eventBus;
    }

    [HttpGet("{orderId:Guid}/listen")]
    public async Task<ActionResult<IEnumerable<Notification>>> ListenEvents(Guid orderId)
    {
        var events = await eventBus.MessagesInOrder(orderId);

        return Ok(events);
    }
    
    
    [HttpPut("{orderId:Guid}/add")]
    public async Task<ActionResult<Notification>> AddEventToOrer(Guid orderId, [FromBody] AddEventRequset requset)
    {
        var token = Token.FromHeaders(HttpContext.Request.Headers);
        var identity = (await identityService.GetIdentity(token)).ValueOrThrow();

        var notification = await eventBus.Publish(
            requset.Channel,
            JsonSerializer.Serialize(requset.Data),
            identity.Id,
            orderId,
            requset.ParrentMessageId
        );

        return Created($"api/order/{orderId}/listen", notification);
    }

    [HttpDelete("{orderId:Guid}/close")]
    public async Task<ActionResult> CloseOrder(Guid orderId, [FromBody] CloseOrderRequset requset)
    {
        var token = Token.FromHeaders(HttpContext.Request.Headers);
        var identity = (await identityService.GetIdentity(token)).ValueOrThrow();

        await eventBus.Close(orderId);
        
        
        var notification = await eventBus.Publish(
            requset.Channel,
            JsonSerializer.Serialize(requset.Data),
            identity.Id,
            orderId,
            requset.ParrentMessageId
        );
        
        return Ok();
    }

    [HttpPut("{orderId:Guid}/subscribe")]
    public async Task<ActionResult> Subscribe(Guid orderId)
    {
        var token = Token.FromHeaders(HttpContext.Request.Headers);
        var identity = (await identityService.GetIdentity(token)).ValueOrThrow();

        await eventBus.Subscribe(identity.Id, orderId);

        return Ok();
    }
}