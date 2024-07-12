using System.Reflection;
using Application.Interfaces;
using Application.Services;
using Dtos.Mappings;
using External.Providers;

namespace Api.Extensions;

public static class DependencyInjection
{
    public static void AddDependencyInjections( this IServiceCollection services )
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(RainfallMapping)));
        services.AddScoped<IRainfallService, RainfallService>();
        services.AddScoped<IEnvironmentDataApi, EnvironmentDataApi>();
    }
}