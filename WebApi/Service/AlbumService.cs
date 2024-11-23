using WebApi.Models;

namespace WebApi.Service;

public interface IAlbumService
{
    Task<IEnumerable<Album>> GetAlbums();
}
public class AlbumService : IAlbumService
{
    private readonly ILogger<Album> _logger;

    public AlbumService(ILogger<Album> logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<Album>> GetAlbums()
    {
        try
        {
            var http = new HttpClient();

            var response = await http.GetAsync("https://jsonplaceholder.typicode.com/albums");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Album>>();

            return result ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting albums");
            throw;
        }
    }
}
