using System.Net;
using Domain.Exceptions;
using Newtonsoft.Json;

namespace External.Shared;

public abstract class ApiBase(
    IHttpClientFactory _httpClientFactory,
    string _clientName)
{
    protected async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken)
    {
        using var client = _httpClientFactory.CreateClient(_clientName);
        var response = await client.GetAsync(url, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
        if(response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new ValidationException("", await response.Content.ReadAsStringAsync(cancellationToken));
        }

        throw new ApiException(await response.Content.ReadAsStringAsync(cancellationToken));
    }
}