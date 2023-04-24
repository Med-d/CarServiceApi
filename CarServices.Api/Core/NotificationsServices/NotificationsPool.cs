using CarServices.Api.Models.Notifications;

namespace CarServices.Api.Core.NotificationsServices;

public class NotificationEvent
{
    public NotificationEvent(string schema, Type eventType)
    {
        Schema = schema;
        EventType = eventType;
    }

    public string Schema { get; }
    public Type EventType { get; }
}

public static class NotificationsPool
{
    public static IEnumerable<NotificationEvent> GetEvents() =>
        new[]
        {
            CreationOrder,
            Message
        };

    public static readonly NotificationEvent CreationOrder = new("create-order", typeof(CreationOrder));
    public static readonly NotificationEvent Message = new("message", typeof(Message));
    public static readonly NotificationEvent CloseOrder = new("close-order", typeof(CloseOrder));
}