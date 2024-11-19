using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Data;
using WebApi.IntegrationTests.Models;
using WebApi.Models;

namespace WebApi.IntegrationTests;

public class WebApiTest : IClassFixture<WebApiFactory>
{
    private readonly HttpClient _client;
    private readonly WebApiFactory _factory;

    public WebApiTest(WebApiFactory factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
    }

    [Theory]
    [InlineData("sum", 75)]      
    [InlineData("average", 25)] 
    public async Task CalculateEndpoint_ReturnsCorrectResult(string operation, double expected)
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Numbers.AddRange(
                new Number { Id = 100, Value = 15 },
                new Number { Id = 101, Value = 25 },
                new Number { Id = 102, Value = 35 }
            );
            await dbContext.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync($"/calculate?operation={operation}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CalculationResult>();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected, result.Result);

        // Cleanup
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Numbers.RemoveRange(dbContext.Numbers);
            await dbContext.SaveChangesAsync();
        }
    }
}
