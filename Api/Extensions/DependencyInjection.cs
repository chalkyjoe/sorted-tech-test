using System.Reflection;
using Domain.Interfaces;
using Domain.Services;
using External.Providers;

namespace Api.Extensions;

public static class DependencyInjection
{
    public static void AddDependencyInjections( this IServiceCollection services )
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));
        services.AddScoped<IRainfallService, RainfallService>();
        services.AddScoped<IEnvironmentDataApi, EnvironmentDataApi>();
    }
}