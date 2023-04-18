using CarServices.Api.Core.AuthServices;
using CarServices.Api.Core.UnitOfWork;
using CarServices.Api.Core.UnitOfWork.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CarServices.Api.Configure;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddDbContext<CarServiceDbContext>((provider, options) =>
        {
            options.UseSqlServer(
                provider.GetRequiredService<Settings>().DbConnectionString,
                sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); }
            );
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<CryptoHelper>();
        services.AddScoped<IdentityService>();
    }
}