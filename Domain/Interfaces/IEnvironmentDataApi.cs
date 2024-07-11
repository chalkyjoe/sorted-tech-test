using Domain.Models;

namespace Domain.Interfaces;

public interface IEnvironmentDataApi
{
    RainfallResponse GetMeasure(string stationId, int count);
}