using Domain.Models;

namespace Domain.Interfaces;

public interface IRainfallService
{
    Task<RainfallResponse> GetRainfall(  string stationId, int count );
}