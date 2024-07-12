using Application.Interfaces;
using Domain.Exceptions;
using Domain.Models;

namespace Application.Services;

public class RainfallService(IEnvironmentDataApi _environmentDataApi) : IRainfallService
{
    public async Task<EnvironmentDataResponse> GetRainfall(string stationId, int count, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(stationId))
        {
            throw new ValidationException("stationId", "Station ID is required");
        }

        if (count is < 0 or > 100)
        {
            throw new ValidationException( "count", "Count must be within 1 and 100." );
        }

        var readings = await _environmentDataApi.GetMeasure(stationId, count, cancellationToken);
        if (!readings.Items.Any())
        {
            throw new NotFoundException("No readings found for the specified station Id");
        }
        
        return readings;
    }
}
