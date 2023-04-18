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
        
}