namespace CarServices.Api.Models.Notifications;

public interface IEvent
{
    bool ValidateContent();
    string GetJson();
}