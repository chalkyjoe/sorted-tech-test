using External.Providers;
using Polly;

namespace Api.Extensions;

public static class HttpClients
{
    public static void AddHttpClients(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient(nameof(EnvironmentDataApi),client =>
        {
            client.BaseAddress = new Uri(config["EnvironmentDataApiUrl"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));;
    }
}