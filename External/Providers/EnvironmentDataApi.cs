using Domain.Interfaces;
using Domain.Models;
using External.Shared;
using Microsoft.Extensions.Logging;

namespace External.Providers;

public class EnvironmentDataApi(IHttpClientFactory _httpClientFactory) : ApiBase(_httpClientFactory, nameof(EnvironmentDataApi)), IEnvironmentDataApi
{
    public Task<RainfallResponse> GetMeasure(string stationId, int count, CancellationToken cancellationToken) => 
        GetAsync<RainfallResponse>( $"id/stations/{stationId}/readings?_limit={count}", cancellationToken );
}