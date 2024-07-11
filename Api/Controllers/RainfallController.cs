using System.Net;
using Api.Exceptions;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.Models;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RainfallController : ControllerBase
{
    public RainfallController( IRainfallService rainfallService, IMapper mapper )
    {
        _rainfallService = rainfallService;
        _mapper = mapper;
    }
    
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

        if (count is < 0 or > 100)
        {
            throw new ControllerException( "count", HttpStatusCode.BadRequest, "Count must be within 1 and 100." );
        }

        var response = _rainfallService.GetRainfall(stationId, count);

        if (!response.Items.Any())
        {
            throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No readings found for the specified station Id");
        }

        var readings = _mapper.Map<List<RainfallReading>>(response.Items);
        return Ok(new RainfallReadingResponse { Readings = readings });
    }
    private readonly IRainfallService _rainfallService;
    private readonly IMapper _mapper;
}
