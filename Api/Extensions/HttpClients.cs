using External.Providers;

namespace Api.Extensions;

public static class HttpClients
{
    public static void AddHttpClients(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient(nameof(EnvironmentDataApi),client =>
        {
            client.BaseAddress = new Uri(config["EnvironmentDataApiUrl"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
    }
}