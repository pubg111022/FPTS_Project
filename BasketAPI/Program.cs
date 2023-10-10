using System.Configuration;
using System;
using b3.Core.Databases;
using b3.Core.IRepositories;
using b3.Core.Repositories;
using b3.Core.IServices;
using b3.Services;
using BasketAPI.Core.Databases.InMemory;
using BasketAPI.Core.Databases;
using BasketAPI.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOracle<BasketDbContext>(configuration.GetConnectionString("OracleConnection"));
builder.Services.AddScoped<IBasketItemRepository, BasketItemRepository>();
builder.Services.AddScoped<IBasketItemService, BasketItemService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddSingleton<BasketMemory>();
builder.Services.AddSingleton<BasketItemMemory>();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.LoadDataToMemory<BasketItemMemory, BasketDbContext>((data, dbContext) =>
{
    new BasketItemMemorySeedAsync().SeedAsync(data, dbContext).Wait();
});
app.LoadDataToMemory<BasketMemory, BasketDbContext>((data, dbContext) =>
{
    new BasketMemorySeedAsync().SeedAsync(data, dbContext).Wait();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
