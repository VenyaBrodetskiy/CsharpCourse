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
builder.Services.AddScoped<EmailNotificationService>();
builder.Services.AddScoped<SmsNotificationService>();
builder.Services.AddScoped<PushNotificationService>();

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
        [FromServices] EmailNotificationService email, 
        [FromServices] SmsNotificationService sms, 
        [FromServices] PushNotificationService push) =>
{
    var to = "mom";
    var message = "I am ok";
    switch (type.ToLower())
    {
        case "email":
            await email.SendAsync(to, message);
            break;
        case "sms":
            await sms.SendAsync(to, message);
            break;
        case "push":
            await push.SendAsync(to, message);
            break;
        default:
            throw new NotSupportedException("Notification type not supported");
    }

    return Results.Ok();
})
.WithName("notification");

app.Run();

public partial class Program { }
