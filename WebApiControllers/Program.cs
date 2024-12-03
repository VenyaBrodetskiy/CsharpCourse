using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApiControllers;
using WebApiControllers.Data;
using WebApiControllers.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
