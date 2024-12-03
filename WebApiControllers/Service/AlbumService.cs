using WebApiControllers.Models;

namespace WebApiControllers.Service;

public interface IAlbumService
{
    Task<IEnumerable<Album>> GetAlbums();
}
public class AlbumService : IAlbumService
{
    private readonly ILogger<Album> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public AlbumService(ILogger<Album> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Album>> GetAlbums()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("AlbumsApi");
            var response = await httpClient.GetAsync("/albums");

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
