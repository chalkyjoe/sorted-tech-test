using System.Net;
using Domain.Exceptions;
using Domain.Services;

namespace UnitTests.Tests;

public class RainfallServiceTests
{
    public RainfallServiceTests()
    {
        _sut = new RainfallService();
    }
    
    [Fact]
    public void WhenGetRainfallMethodCalled_WithStationId_ReturnResults()
    {
        var response = _sut.GetRainfall("5002", 10);
        Assert.True(response.Items.Any());
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_CountIs5_Return5Results()
    {
        var response = _sut.GetRainfall("5002", 5);
        Assert.Equal(5, response.Items.Count);
    }
    
    [Fact]
    public void WhenGetRainfallMethodCalled_WithNoStationId_ReturnBadRequest()
    {
        var exception = Assert.Throws<PropertyException>(() =>
            {
                var response = _sut.GetRainfall("", 10);
            }
        );
        Assert.Equal(exception.StatusCode, HttpStatusCode.BadRequest);
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_CountIsNegative_ReturnBadRequest()
    {
        var exception = Assert.Throws<PropertyException>(() =>
        {
            var response = _sut.GetRainfall("55", -5);
        });
        Assert.Equal(exception.StatusCode, HttpStatusCode.BadRequest);
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_CountIsOver100_ReturnBadRequest( )
    {
        var exception = Assert.Throws<PropertyException>(( ) =>
        {
            var response = _sut.GetRainfall("55", 101);
        });
        Assert.Equal(exception.StatusCode, HttpStatusCode.BadRequest);
    }
    
    //TODO: Once an interface is realised, create test for no results.
    private RainfallService _sut;
}