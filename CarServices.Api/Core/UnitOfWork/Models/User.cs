using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CarServices.Api.Core.UnitOfWork.Models;

public class User
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Token { get; set; } = null!;
    public Role Role { get; set; }
}