using CarServices.Api.Core.UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace CarServices.Api.Core.UnitOfWork.DbContexts;

public class CarServiceDbContext : DbContext
{
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<NotificationSubscriber> NotificationSubscribers { get; set; } = null!;


    public CarServiceDbContext(DbContextOptions<CarServiceDbContext> options)
        : base(options)
    {
    }
}