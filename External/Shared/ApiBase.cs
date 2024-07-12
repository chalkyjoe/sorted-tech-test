using System.Net;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.Shared;

public abstract class ApiBase(
    IHttpClientFactory _httpClientFactory,
    ILogger<ApiBase> _logger,
    string _clientName)
{
    protected async Task<T> GetAsync<T>( string url )
    {
        var client = _httpClientFactory.CreateClient(_clientName);
        var response = await client.GetAsync( url );
        if(!response.IsSuccessStatusCode)
        {
            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ValidationException( "", await response.Content.ReadAsStringAsync() );
            }

            throw new ApiException( await response.Content.ReadAsStringAsync() );
        }
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }
}