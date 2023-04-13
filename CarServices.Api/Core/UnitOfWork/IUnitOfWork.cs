using CarServices.Api.Core.UnitOfWork.Models;
using CarServices.Api.Core.UnitOfWork.Repositories;

namespace CarServices.Api.Core.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<Notification> NotificationRepository { get; }
    IRepository<NotificationSubscriber> NotificationSubscriberRepository { get; }
    IRepository<User> UserRepository { get; }
    IRepository<Role> RoleRepository { get; }
    IRepository<Permission> PermissionRepository { get; }
    IRepository<RolePermission> RolePermissionsvRepository { get; }

    Task<bool> Complete();
}