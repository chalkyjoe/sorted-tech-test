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
        // Act/Assert
        Assert.Throws<ValidationException>(() =>
            {
                var response = _sut.GetRainfall("", 10);
            }
        );
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_CountIsNegative_ReturnBadRequest()
    {
        // Act/Assert
        Assert.Throws<ValidationException>(() =>
        {
            _sut.GetRainfall("55", -5);
        });
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_CountIsOver100_ReturnBadRequest()
    {
        // Act/Assert
        Assert.Throws<ValidationException>(( ) =>
        {
            _sut.GetRainfall("55", 101);
        });
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_MockNoResults_ReturnNotFound( )
    {
        // Arrange
        _environmentDataApi.GetMeasure( "55", 10 ).Returns( new RainfallResponse { Items = new List<Item>() } );
        // Act/Assert
        Assert.Throws<NotFoundException>( ( ) =>
        {
            _sut.GetRainfall( "55", 10 );
        });
    }
    
    private RainfallService _sut;
    private readonly IEnvironmentDataApi _environmentDataApi;
}