using Domain.Models;

namespace Domain.Interfaces;

public interface IEnvironmentDataApi
{
    Task<RainfallResponse> GetMeasure( string stationId, int count, CancellationToken cancellationToken);
}