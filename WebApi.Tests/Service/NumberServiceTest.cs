using Moq;
using WebApi.Service;

namespace WebApi.Tests.Service;

public class NumberServiceTest(NumberServiceFixture fixture) : IClassFixture<NumberServiceFixture>
{
    private readonly INumberService _numberService = fixture.NumberService;
    private readonly Mock<IDbService> _mockDb = fixture.MockDbService;

    [Fact]
    public async Task CalculateAverageAsync_ReturnsCorrectSum_WhenNumbersExist()
    {
        // Arrange
        var numbers = new List<int> { 10, 20, 30 };
        _mockDb
            .Setup(db => db.GetNumbersAsync())
            .ReturnsAsync(numbers);

        // Act
        var result = await _numberService.CalculateAverageAsync();

        // Assert
        Assert.Equal(numbers.Average(), result);
    }

    [Fact]
    public async Task CalculateAverageAsync_ReturnsZero_WhenNumbersDoNotExist()
    {
        // Arrange
        var numbers = new List<int>();
        //var numbers = new List<int> { 20, 30, 40 };

        _mockDb
            .Setup(db => db.GetNumbersAsync())
            .ReturnsAsync(numbers);

        // Act
        var result = await _numberService.CalculateAverageAsync();

        // Assert
        Assert.Equal(0, result);
    }
}