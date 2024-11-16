using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Service;

public interface IDbService
{
    Task<List<int>> GetNumbersAsync();
}

public class DbService(ILogger<DbService> logger, AppDbContext dbContext) : IDbService
{
    public async Task<List<int>> GetNumbersAsync()
    {
        logger.LogInformation("Started getting entities");
        var result = await dbContext.Numbers.Select(n => n.Value).ToListAsync();

        return result;
    }
}
