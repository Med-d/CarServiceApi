using CarServices.Api.Core.UnitOfWork.Models;
using CarServices.Api.Core.UnitOfWork.Repositories;

namespace CarServices.Api.Core.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<Notification> NotificationRepository { get; }
    IRepository<NotificationSubscriber> NotificationSubscriberRepository { get; }
    IRepository<User> UserRepository { get; }

    Task<bool> Complete();
}