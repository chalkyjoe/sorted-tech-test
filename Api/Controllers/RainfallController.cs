using Application.Interfaces;
using AutoMapper;
using Dtos.Client;
using Dtos.Error;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<RainfallReadingResponse>> GetRainfallReadings(string stationId, [FromQuery] int count = 10, CancellationToken cancellationToken = default)
    {
        var response = await _rainfallService.GetRainfall(stationId, count, cancellationToken);

        var readings = _mapper.Map<List<RainfallReading>>(response.Items);
        
        return Ok(new RainfallReadingResponse { Readings = readings });
    }
    
    private readonly IRainfallService _rainfallService;
    private readonly IMapper _mapper;
}
