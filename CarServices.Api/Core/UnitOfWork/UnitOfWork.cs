using CarServices.Api.Core.UnitOfWork.DbContexts;
using CarServices.Api.Core.UnitOfWork.Models;
using CarServices.Api.Core.UnitOfWork.Repositories;

namespace CarServices.Api.Core.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly CarServiceDbContext context;

    public UnitOfWork(CarServiceDbContext context)
    {
        this.context = context;
    }

    #region Repositories

    private IRepository<Notification>? notificationRepository;
    public IRepository<Notification> NotificationRepository =>
        notificationRepository ??= new Repository<Notification>(context);

    private IRepository<NotificationSubscriber>? notificationSubscriberRepository;
    public IRepository<NotificationSubscriber> NotificationSubscriberRepository =>
        notificationSubscriberRepository ??= new Repository<NotificationSubscriber>(context);

    private IRepository<User>? userRepositry;
    public IRepository<User> UserRepository =>
        userRepositry ??= new Repository<User>(context);

    #endregion
    
    public async Task<bool> Complete() => await context.SaveChangesAsync() > 0;

    #region Disposing
    
    private bool disposed;

    private void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
            disposed = true;
        }
    }
 
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    #endregion
    

}