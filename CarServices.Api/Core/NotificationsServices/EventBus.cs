using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using CarServices.Api.Core.Exceptions;
using CarServices.Api.Core.UnitOfWork;
using CarServices.Api.Core.UnitOfWork.Models;
using CarServices.Api.Models.Notifications;
using OperationResult;

namespace CarServices.Api.Core.NotificationsServices;

public class EventBus
{
    private readonly IUnitOfWork unitOfWork;

    public EventBus(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Notification> Publish(string channel, string jsonData, int userId, Guid? orderId = null, Guid? parrentMessageId = null)
    {
        var notificationEvent = NotificationsPool.GetEvents().First(item => item.Schema.Equals(channel));
        if (JsonSerializer.Deserialize(jsonData, notificationEvent.EventType) is not IEvent eventData)
            throw new NullReferenceException();
        if (!eventData.ValidateContent())
            throw new InvalidDataException();

        var notificationRepo = unitOfWork.NotificationRepository;
        var item = new Notification()
        {
            OrderId = orderId ?? Guid.NewGuid(),
            MessageId = Guid.NewGuid(),
            PastMessageId = parrentMessageId ?? Guid.Empty,
            UserId = userId,
            Schema = notificationEvent.Schema,
            Data = eventData.GetJson(),
            Created = DateTime.Now
        };
        
        notificationRepo.Add(item);

        await unitOfWork.Complete();
        return item;
    }

    public async Task<bool> Subscribe(int subscriberId, Guid orederId)
    {
        if ((await unitOfWork.NotificationSubscriberRepository
                .GetWhere(item => item.OrderId.Equals(orederId))).Any(
                item => item.isClosed))
            return false;

        var item = new NotificationSubscriber()
        {
            OrderId = orederId,
            SubscriberId = subscriberId,
            isClosed = false,
        };
        unitOfWork.NotificationSubscriberRepository.Add(item);
        return await unitOfWork.Complete();
    }

    public async Task<IEnumerable<Guid>> ListenSubscribes(int subscriberId)
    {
        var subsRepo = unitOfWork.NotificationSubscriberRepository;
        
        var orders = (await subsRepo.GetWhere(item => 
            !item.isClosed && item.SubscriberId.Equals(subscriberId))).Select(item => item.OrderId);

        await unitOfWork.Complete();
        return orders;
    }

    public async Task<IEnumerable<Notification>> MessagesInOrder(Guid orderId)
    {
        var notificationRepo = unitOfWork.NotificationRepository;

        var notifications = await notificationRepo.GetWhere(item => item.OrderId.Equals(orderId));

        await unitOfWork.Complete();

        return notifications;
    }

    public async Task<bool> Close(Guid orderId)
    {
        var subsRepo = unitOfWork.NotificationSubscriberRepository;
        var subsctibers = (await subsRepo
                .GetWhere(item => item.OrderId.Equals(orderId)))
            .ToArray();

        if (!subsctibers.Any())
            throw new OrderNotFoundException();

        foreach (var subsctiber in subsctibers)
        {
            subsctiber.isClosed = true;
            subsRepo.Update(subsctiber);
        }

        return await unitOfWork.Complete();
    }
}