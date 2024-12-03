using Microsoft.AspNetCore.Mvc;
using WebApiControllers.Service;

namespace WebApiControllers.Controllers;

[Route("[controller]")]
[ApiController]
public class FactoryDemoController(IAlbumService albumService, INotificationFactory notificationFactory)
    : ControllerBase
{
    [HttpGet("/albums")]
    public async Task<IActionResult> GetAlbums()
    {
        var result = await albumService.GetAlbums();
        return Ok(new
        {
            Result = result
        });
    }

    [HttpGet("/notify-mom")]
    public async Task<IActionResult> NotifyMom([FromQuery] string type)
    {
        const string to = "mom";
        const string message = "I am ok";

        var notificationService = notificationFactory.GetNotificationService(type);

        await notificationService.SendAsync(to, message);

        return Ok();
    }
}
