using Domain.Models;

namespace Application.Interfaces;

public interface IRainfallService
{
    Task<EnvironmentDataResponse> GetRainfall(string stationId, int count, CancellationToken cancellationToken);
}