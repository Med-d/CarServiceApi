using System.Text.Json;

namespace CarServices.Api.Models.Notifications;

public record Message(
    string SenderName,
    string Value
) : IEvent
{
    public bool ValidateContent()
    {
        return true;
    }

    public string GetJson() => JsonSerializer.Serialize(this);
}