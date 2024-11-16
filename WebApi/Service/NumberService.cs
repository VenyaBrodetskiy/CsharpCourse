namespace WebApi.Service;

public interface INumberService
{
    Task<double> CalculateAverageAsync();
    Task<double> CalculateSumAsync();
}

public class NumberService(IDbService db, ILogger<NumberService> logger) : INumberService
{
    public async Task<double> CalculateAverageAsync()
    {
        logger.LogInformation("Started calculating average");

        var numbers = await db.GetNumbersAsync();

        if (numbers.Count == 0)
        {
            return 0;
        }

        return numbers.Average();
    }

    public async Task<double> CalculateSumAsync()
    {
        logger.LogInformation("Started calculating sum");

        var numbers = await db.GetNumbersAsync();
        
        if (numbers.Count == 0)
        {
            return 0;
        }

        return numbers.Sum();
    }
}
