using Microsoft.AspNetCore.Mvc;
using WebApi.Service;

namespace WebApi.Endpoints;

public static class CalculationEndpoints
{
    public static WebApplication MapCalculationEndpoints(this WebApplication app)
    {
        app.MapGet("/calculate", Calculate).WithName("calculate");

        return app;
    }

    private static async Task<IResult> Calculate(
        [FromServices] INumberService numberService, 
        [FromQuery] string operation)
    {
        var result = operation.ToLower() switch
        {
            "average" => await numberService.CalculateAverageAsync(),
            "sum" => await numberService.CalculateSumAsync(),
            _ => throw new NotSupportedException("Operation not supported")
        };

        return Results.Ok(new
        {
            Result = result
        });
    }
}
