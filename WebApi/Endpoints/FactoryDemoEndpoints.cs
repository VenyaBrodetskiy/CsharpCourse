using Microsoft.AspNetCore.Mvc;
using WebApi.Service;

namespace WebApi.Endpoints;

public static class FactoryDemoEndpoints
{
    public static WebApplication MapFactoryDemoEndpoints(this WebApplication app)
    {
        app.MapGet("/albums", GetAlbums).WithName("albums");
        app.MapGet("/notify-mom", NotifyMom).WithName("notification");

        return app;
    }

    private static async Task<IResult> GetAlbums([FromServices] IAlbumService service)
    {
        var result = await service.GetAlbums();

        return Results.Ok(new
        {
            Result = result
        });
    }

    private static async Task<IResult> NotifyMom(
        [FromQuery] string type,
        [FromServices] INotificationFactory factory)
    {
        const string to = "mom";
        const string message = "I am ok";

        var notificationService = factory.GetNotificationService(type);

        await notificationService.SendAsync(to, message);

        return Results.Ok();
    }
}
