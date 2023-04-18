using System.ComponentModel;
using CarServices.Api.Core.UnitOfWork.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DbController : Controller
{
    private readonly CarServiceDbContext context;

    public DbController(CarServiceDbContext context)
    {
        this.context = context;
    }
    
    [HttpGet("create")]
    [Description("Для создания БД из описанных дбСетов. Не миграция")]
    public async Task<IActionResult> Get()
    {
        await context.Database.EnsureDeletedAsync();
        var result = await context.Database.EnsureCreatedAsync();
        return Ok(result);
    }   
}