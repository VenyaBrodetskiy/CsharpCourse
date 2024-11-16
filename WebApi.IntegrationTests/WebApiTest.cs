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

    [Fact]
    public async Task CalculateSumEndpoint_ReturnsCorrectSum()
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
        var response = await _client.GetAsync("/calculate?operation=sum");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CalculationResult>();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(75, result.Result);
    }
}
