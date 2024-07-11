using System.Reflection;
using Domain.Interfaces;
using Domain.Services;

namespace Api.Extensions;

public static class DependencyInjection
{
    public static void AddDependencyInjections( this IServiceCollection services, IConfiguration configuration )
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));
        services.AddScoped<IRainfallService, RainfallService>();
    }
}