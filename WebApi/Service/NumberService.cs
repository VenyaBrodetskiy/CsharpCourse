using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Service;

public class NumberService(AppDbContext dbContext)
{
    public async Task<double> CalculateAverageAsync()
    {
        var numbers = await dbContext.Numbers.Select(n => n.Value).ToListAsync();

        if (numbers.Count == 0)
        {
            return 0;
        }

        return numbers.Average();
    }

    public async Task<double> CalculateSumAsync()
    {
        var numbers = await dbContext.Numbers.Select(n => n.Value).ToListAsync();
        
        if (numbers.Count == 0)
        {
            return 0;
        }

        return numbers.Sum();
    }
}
