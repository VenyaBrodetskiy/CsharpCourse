using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApi;
using WebApi.Data;
using WebApi.Endpoints;
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

builder.Services.AddNotificationServices();

builder.Services.AddHttpClient("AlbumsApi", client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
});

builder.Services.AddHttpClient();

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

app.MapCalculationEndpoints();
app.MapFactoryDemoEndpoints();

app.Run();

public partial class Program { }
