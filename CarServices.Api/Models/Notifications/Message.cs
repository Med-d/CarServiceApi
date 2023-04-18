using Newtonsoft.Json;

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

    public string GetJson() => JsonConvert.SerializeObject(this);
}