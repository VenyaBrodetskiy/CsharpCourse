using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using WebApi.Service;

namespace WebApi.Tests.Service;

public class NumberServiceTest
{

    [Fact]
    public async Task CalculateAverageAsync_ReturnsCorrectSum_WhenNumbersExist()
    {
        // Arrange
        //var mockDbService = new Mock<IDbService>();
        var mockDbService = Substitute.For<IDbService>();
        //var mockLogger = new Mock<ILogger<NumberService>>();
        var mockLogger = Substitute.For<ILogger<NumberService>>();

        var numbers = new List<int> { 10, 20, 30 };
        //mockDbService
        //    .Setup(db => db.GetNumbersAsync())
        //    .ReturnsAsync(numbers);

        mockDbService.GetNumbersAsync().Returns(numbers);

        var numberService = new NumberService(mockDbService, mockLogger);

        // Act
        var result = await numberService.CalculateAverageAsync();

        // Assert
        Assert.Equal(numbers.Average(), result);
    }

    [Fact]
    public async Task CalculateAverageAsync_ReturnsZero_WhenNumbersDoNotExist()
    {
        // Arrange
        var mockDbService = new Mock<IDbService>();
        var mockLogger = new Mock<ILogger<NumberService>>();

        var numbers = new List<int>();
        mockDbService
            .Setup(db => db.GetNumbersAsync())
            .ReturnsAsync(numbers);
        var numberService = new NumberService(mockDbService.Object, mockLogger.Object);

        // Act
        var result = await numberService.CalculateAverageAsync();

        // Assert
        Assert.Equal(0, result);
    }
}