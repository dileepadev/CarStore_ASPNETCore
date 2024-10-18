using CarStore_MinimalAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CarDB>(opt => opt.UseInMemoryDatabase("CarList"));
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Car Store API",
        Description = "API for managing a list of cars on the store.",
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<CarDB>();
    dbContext.Database.EnsureCreated();
}

app.MapGet("/carlist", async (CarDB db) =>
   await db.Cars.ToListAsync())
   .WithTags("Get all cars");

app.MapGet("/carlist/available", async (CarDB db) =>
    await db.Cars.Where(t => t.IsAvailable).ToListAsync())
    .WithTags("Get all available cars");

app.MapGet("/carlist/{id}", async (int id, CarDB db) =>
    await db.Cars.FindAsync(id)
        is CarModel car
            ? Results.Ok(car)
            : Results.NotFound())
    .WithTags("Get car by Id");

app.MapPost("/carlist", async (CarModel car, CarDB db) =>
{
    db.Cars.Add(car);
    await db.SaveChangesAsync();

    return Results.Created($"/carlist/{car.Id}", car);
})
    .WithTags("Add car to list");

app.MapPut("/carlist/{id}", async (int id, CarModel inputCar, CarDB db) =>
{
    var car = await db.Cars.FindAsync(id);

    if (car is null) return Results.NotFound();

    car.Brand = inputCar.Brand;
    car.Model = inputCar.Model;
    car.Color = inputCar.Color;
    car.Owner = inputCar.Owner;
    car.Price = inputCar.Price;
    car.IsAvailable = inputCar.IsAvailable;

    await db.SaveChangesAsync();

    return Results.NoContent();
})
    .WithTags("Update car by Id");

app.MapDelete("/carlist/{id}", async (int id, CarDB db) =>
{
    if (await db.Cars.FindAsync(id) is CarModel car)
    {
        db.Cars.Remove(car);
        await db.SaveChangesAsync();
        return Results.Ok(car);
    }

    return Results.NotFound();
})
    .WithTags("Delete car by Id");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();