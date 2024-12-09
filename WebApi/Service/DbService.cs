using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Service;

public interface IDbService
{
    Task<List<int>> GetNumbersAsync();
    Task<Number> GetNumberByIdFilterInDb(int id);
    Task<Number> GetNumberByIdFilterInMemory(int id);
}

public class DbService(ILogger<DbService> logger, AppDbContext dbContext) : IDbService
{
    public async Task<List<int>> GetNumbersAsync()
    {
        logger.LogInformation("Started getting entities");
        var result = await dbContext.Numbers.Select(n => n.Value).ToListAsync();

        return result;
    }

    public async Task<Number> GetNumberByIdFilterInDb(int id)
    {
        var queryable = dbContext.Numbers
            .Where(n => n.Id == id);

        var result = await queryable.FirstOrDefaultAsync();
        return result;
    }

    public async Task<Number> GetNumberByIdFilterInMemory(int id)
    {
        var enumerable = dbContext.Numbers.AsEnumerable();

        var result = enumerable
            .FirstOrDefault(n => n.Id == id);

        return result;
    }
}
