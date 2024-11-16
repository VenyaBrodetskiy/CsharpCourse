using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Numbers"));
builder.Services.AddScoped<NumberService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
    dbContext.Numbers.AddRange(
        new Entity { Id = 1, Value = 10 },
        new Entity { Id = 2, Value = 20 },
        new Entity { Id = 3, Value = 30 }
    );
    dbContext.SaveChanges();
}

app.MapPost("/calculate", async ([FromServices] NumberService numberService, [FromQuery] string operation) =>
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

app.Run();