using System.Net;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.Services;
using NSubstitute;

namespace UnitTests.Tests;

public class RainfallServiceTests
{
    public RainfallServiceTests()
    {
        _environmentDataApi = Substitute.For<IEnvironmentDataApi>();
        _sut = new RainfallService(_environmentDataApi);
    }
    
    [Fact]
    public void WhenGetRainfallMethodCalled_WithStationId_ReturnResults()
    {
        // Arrange
        _environmentDataApi.GetMeasure( "5002", 10 )
            .Returns( new RainfallResponse() { Items = new List<Item> { new Item() } } );
        // Act
        var response = _sut.GetRainfall("5002", 10);
        // Assert
        Assert.True(response.Items.Any());
    }
    
    [Fact]
    public void WhenGetRainfallMethodCalled_WithNoStationId_ReturnBadRequest()
    {
        // Act
        var exception = Assert.Throws<PropertyException>(() =>
            {
                var response = _sut.GetRainfall("", 10);
            }
        );
        // Assert
        Assert.Equal(exception.StatusCode, HttpStatusCode.BadRequest);
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_CountIsNegative_ReturnBadRequest()
    {
        // Act
        var exception = Assert.Throws<PropertyException>(() =>
        {
            var response = _sut.GetRainfall("55", -5);
        });
        // Assert
        Assert.Equal(exception.StatusCode, HttpStatusCode.BadRequest);
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_CountIsOver100_ReturnBadRequest()
    {
        // Act
        var exception = Assert.Throws<PropertyException>(( ) =>
        {
            var response = _sut.GetRainfall("55", 101);
        });
        // Assert
        Assert.Equal(exception.StatusCode, HttpStatusCode.BadRequest);
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_MockNoResults_ReturnNotFound( )
    {
        _environmentDataApi.GetMeasure( "55", 10 ).Returns( new RainfallResponse { Items = new List<Item>() } );
        var exception = Assert.Throws<HttpStatusCodeException>( ( ) =>
        {
            var response = _sut.GetRainfall( "55", 10 );
        });
        // Act

        Assert.Equal(exception.StatusCode, HttpStatusCode.NotFound);
    }
    
    //TODO: Once an interface is realised, create test for no results.
    private RainfallService _sut;
    private readonly IEnvironmentDataApi _environmentDataApi;
}