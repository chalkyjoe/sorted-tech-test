using Domain.Models;

namespace Application.Interfaces;

public interface IEnvironmentDataApi
{
    Task<EnvironmentDataResponse> GetMeasure( string stationId, int count, CancellationToken cancellationToken);
}