using Application.Interfaces;
using Domain.Models;
using External.Shared;

namespace External.Providers;

public class EnvironmentDataApi(IHttpClientFactory _httpClientFactory) : ApiBase(_httpClientFactory, nameof(EnvironmentDataApi)), IEnvironmentDataApi
{
    public Task<EnvironmentDataResponse> GetMeasure(string stationId, int count, CancellationToken cancellationToken) => 
        GetAsync<EnvironmentDataResponse>( $"id/stations/{stationId}/readings?_limit={count}", cancellationToken );
}