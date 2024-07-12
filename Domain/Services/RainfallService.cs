using System.Net;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Services;

public class RainfallService ( IEnvironmentDataApi _environmentDataApi ) : IRainfallService
{
    public RainfallResponse GetRainfall(string stationId, int count)
    {
        if (string.IsNullOrWhiteSpace(stationId))
        {
            throw new ValidationException("stationId", "Station ID is required");
        }

        if (count is < 0 or > 100)
        {
            throw new ValidationException( "count", "Count must be within 1 and 100." );
        }

        var readings = _environmentDataApi.GetMeasure( stationId, count );
        if (!readings.Items.Any())
        {
            throw new NotFoundException("No readings found for the specified station Id");
        }
        
        return readings;
    }
}
