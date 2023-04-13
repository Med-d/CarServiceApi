using System.ComponentModel.DataAnnotations;

namespace CarServices.Api.Core.UnitOfWork.Models;

public class Permission
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
}