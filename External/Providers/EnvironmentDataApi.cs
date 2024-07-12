using Domain.Interfaces;
using Domain.Models;
using External.Shared;
using Microsoft.Extensions.Logging;

namespace External.Providers;

public class EnvironmentDataApi( IHttpClientFactory _httpClientFactory, ILogger<EnvironmentDataApi> _logger ) : ApiBase(_httpClientFactory, _logger, nameof(EnvironmentDataApi)), IEnvironmentDataApi
{
    public Task<RainfallResponse> GetMeasure(string stationId, int count) => 
        GetAsync<RainfallResponse>( $"id/stations/{stationId}/readings?_limit={count}" );
}