using WebApi.Models;

namespace WebApi.Service;

public interface IAlbumService
{
    Task<IEnumerable<Album>> GetAlbums();
}
public class AlbumService : IAlbumService
{
    private readonly ILogger<Album> _logger;
    private readonly HttpClient _httpClient;

    public AlbumService(ILogger<Album> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Album>> GetAlbums()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/albums");

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
