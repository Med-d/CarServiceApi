using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace CarServices.Api.Models.Notifications;

public record CloseOrder(
    string Title,
    string? Description,
    Guid ParrentMessageId,
    Guid OrderId
) : IEvent
{
    public bool ValidateContent()
    {
        return true;
    }

    public string GetJson() => JsonSerializer.Serialize(this);
}