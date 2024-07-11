using Domain.Models;

namespace Domain.Interfaces;

public interface IRainfallService
{
    RainfallResponse GetRainfall( string stationId, int count );
}