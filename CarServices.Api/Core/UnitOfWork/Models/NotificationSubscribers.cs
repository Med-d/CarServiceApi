using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CarServices.Api.Core.UnitOfWork.Models;

public class NotificationSubscriber
{
    [Key] public Guid Id { get; set; }
    
    [Required] public Guid OrderId { get; set; }
    public virtual Notification Notification { get; set; }

    [Required] public int SubscriberId { get; set; }
    public virtual User User { get; set; }

    public bool isClosed { get; set; }
}