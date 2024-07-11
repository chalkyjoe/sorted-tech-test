using System.Net;
using Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.Models;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RainfallController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<RainfallReadingResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
    [Route("id/{stationId}/readings")]
    public ActionResult<RainfallReadingResponse> GetRainfallReadings(string stationId, [FromQuery] int count = 10)
    {
        if (string.IsNullOrWhiteSpace(stationId))
        {
            throw new ControllerException("stationId", HttpStatusCode.BadRequest, "Station ID is required");
        }

        if (count < 0)
        {
            throw new ControllerException( "count", HttpStatusCode.BadRequest, "Count must not be negative." );
        }
        
        var readings = new List<RainfallReading>
        {
            new RainfallReading { DateMeasured = DateTime.UtcNow, AmountMeasured = 12.34m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-1), AmountMeasured = 5.67m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-2), AmountMeasured = 8.91m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-3), AmountMeasured = 7.12m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-4), AmountMeasured = 3.45m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-5), AmountMeasured = 6.78m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-6), AmountMeasured = 9.01m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-7), AmountMeasured = 10.23m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-8), AmountMeasured = 11.45m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-9), AmountMeasured = 2.34m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-10), AmountMeasured = 4.56m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-11), AmountMeasured = 5.78m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-12), AmountMeasured = 7.89m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-13), AmountMeasured = 8.90m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-14), AmountMeasured = 1.23m },
            new RainfallReading { DateMeasured = DateTime.UtcNow.AddDays(-15), AmountMeasured = 3.45m },
        };

        readings = readings.Take(count).ToList();

        if (!readings.Any())
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No readings found for the specified station Id");
        }

        var response = new RainfallReadingResponse { Readings = readings };
        return Ok(response);
    }
}