using Application.Interfaces;
using Application.Services;
using Domain.Exceptions;
using Domain.Models;
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
    public async Task WhenGetRainfallMethodCalled_WithStationId_ReturnResults()
    {
        // Arrange
        _environmentDataApi.GetMeasure( "5002", 10, CancellationToken.None )
            .Returns( new EnvironmentDataResponse() { Items = new List<Item> { new Item() } } );
        // Act
        var response = await _sut.GetRainfall("5002", 10, CancellationToken.None);
        // Assert
        Assert.True(response.Items.Any());
    }
    
    [Fact]
    public void WhenGetRainfallMethodCalled_WithNoStationId_ReturnBadRequest()
    {
        // Act/Assert
        Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _sut.GetRainfall("", 10, CancellationToken.None);
            }
        );
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_CountIsNegative_ReturnBadRequest()
    {
        // Act/Assert
        Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await _sut.GetRainfall("55", -5, CancellationToken.None);
        });
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_CountIsOver100_ReturnBadRequest()
    {
        // Act/Assert
        Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await _sut.GetRainfall("55", 101, CancellationToken.None);
        });
    }

    [Fact]
    public void WhenGetRainfallMethodCalled_MockNoResults_ReturnNotFound( )
    {
        // Arrange
        _environmentDataApi.GetMeasure("55", 10, CancellationToken.None).Returns(new EnvironmentDataResponse { Items = new List<Item>() });
        // Act/Assert
        Assert.ThrowsAsync<NotFoundException>( async () =>
        {
            await _sut.GetRainfall("55", 10, CancellationToken.None);
        });
    }
    
    private RainfallService _sut;
    private readonly IEnvironmentDataApi _environmentDataApi;
}