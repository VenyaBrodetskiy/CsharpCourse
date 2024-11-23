using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApi.Data;
using WebApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Numbers"));
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<INumberService, NumberService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();

builder.Services.AddSingleton<EmailNotificationService>();
builder.Services.AddSingleton<SmsNotificationService>();
builder.Services.AddSingleton<PushNotificationService>();

builder.Services.AddSingleton<INotificationFactory, NotificationFactory>();

builder.Services.AddHttpClient("AlbumsApi", client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    await dbContext.Database.EnsureCreatedAsync();
//    dbContext.Numbers.AddRange(
//        new Number { Id = 1, Value = 10 },
//        new Number { Id = 2, Value = 20 },
//        new Number { Id = 3, Value = 30 }
//    );
//    dbContext.SaveChanges();
//}

app.MapGet("/calculate", async ([FromServices] INumberService numberService, [FromQuery] string operation) =>
{
    var result = operation.ToLower() switch
    {
        "average" => await numberService.CalculateAverageAsync(),
        "sum" => await numberService.CalculateSumAsync(),
        _ => throw new NotSupportedException("Operation not supported")
    };

    return Results.Ok(new
    {
        Result = result
    });
})
.WithName("calculate");

app.MapGet("/albums", async ([FromServices] IAlbumService service) =>
{
    var result = await service.GetAlbums();

    return Results.Ok(new
    {
        Result = result
    });
})
.WithName("albums");

app.MapGet("/notify-mom", async (
        [FromQuery] string type, 
        [FromServices] INotificationFactory factory) =>
{
    const string to = "mom";
    const string message = "I am ok";

    var notificationService = factory.GetNotificationService(type);

    await notificationService.SendAsync(to, message);

    return Results.Ok();
})
.WithName("notification");

app.Run();

public partial class Program { }
