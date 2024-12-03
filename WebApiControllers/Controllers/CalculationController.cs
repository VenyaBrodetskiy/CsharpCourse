using Microsoft.AspNetCore.Mvc;
using WebApiControllers.Service;

namespace WebApiControllers.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculationController(INumberService numberService) : ControllerBase
{
    [HttpGet("/calculate")]
    public async Task<IActionResult> Calculate([FromQuery] string operation)
    {
        var result = operation.ToLower() switch
        {
            "average" => await numberService.CalculateAverageAsync(),
            "sum" => await numberService.CalculateSumAsync(),
            _ => throw new NotSupportedException("Operation not supported")
        };
        return Ok(new
        {
            Result = result
        });
    }
}
