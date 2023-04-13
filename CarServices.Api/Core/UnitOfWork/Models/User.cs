using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Api.Core.UnitOfWork.Models;

public class User
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Login { get; set; }
    public string Token { get; set; }
    
    public int RoleId { get; set; }
    public virtual Role Role { get; set; }
}