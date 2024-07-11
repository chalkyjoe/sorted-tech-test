using System.Net;
using Api.Controllers;
using Api.Exceptions;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NSubstitute;
using RainfallApi.Models;

namespace UnitTests.Tests;

public class RainfallControllerTests
{
    public RainfallControllerTests()
    {
        _rainfallService = Substitute.For<IRainfallService>();
        _mapper = Substitute.For<IMapper>();
        _sut = new RainfallController(_rainfallService, _mapper);
    }
    
    [Fact]
    public void WhenMeasureMethodCalled_WithStationId_ReturnOk()
    {
        var response = _sut.GetRainfallReadings("5002", 10);
        Assert.Equal((response.Result as IStatusCodeActionResult)?.StatusCode, 200);
    }

    [Fact]
    public void WhenMeasureMethodCalled_CountIs5_Return5Results()
    {
        var response = _sut.GetRainfallReadings("5002", 5).Result;
        var result = Assert.IsType<OkObjectResult>( response );
        var content = Assert.IsType<RainfallReadingResponse>( result.Value );
        Assert.Equal(5, content.Readings.Count);
    }
    
    [Fact]
    public void WhenMeasureMethodCalled_WithNoStationId_ReturnBadRequest()
    {
        var exception = Assert.Throws<ControllerException>(() =>
            {
                var response = _sut.GetRainfallReadings("", 10);
            }
        );
        Assert.Equal(exception.StatusCode, HttpStatusCode.BadRequest);
    }

    [Fact]
    public void WhenMeasureMethodCalled_CountIsNegative_ReturnBadRequest()
    {
        var exception = Assert.Throws<ControllerException>(() =>
        {
            var response = _sut.GetRainfallReadings("55", -5);
        });
        Assert.Equal(exception.StatusCode, HttpStatusCode.BadRequest);
    }

    [Fact]
    public void WhenMeasureMethodCalled_CountIsOver100_ReturnBadRequest( )
    {
        var exception = Assert.Throws<ControllerException>(( ) =>
        {
            var response = _sut.GetRainfallReadings("55", 101);
        });
        Assert.Equal(exception.StatusCode, HttpStatusCode.BadRequest);
    }
    
    //TODO: Once an interface is realised, create test for no results.
    private RainfallController _sut;
    private IRainfallService _rainfallService;
    private IMapper _mapper;
}