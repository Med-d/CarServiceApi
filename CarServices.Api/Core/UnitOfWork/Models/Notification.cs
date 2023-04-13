using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace CarServices.Api.Core.UnitOfWork.Models;

[PrimaryKey(nameof(OrderId), nameof(MessageId))]
public class Notification
{
    [Required] public Guid OrderId { get; set; }
    [Required] public Guid MessageId { get; set; }
    
    public Guid? PastMessageId { get; set; }

    [Required] public string Schema { get; set; }
    [Required] public string Data { get; set; }

    [Required] public DateTime Created { get; set; }
}