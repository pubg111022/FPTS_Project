using ProductAPI.Core.Database;
using ProductAPI.Core.Database.InMemory;
using ProductAPI.Core.IRepositories;
using ProductAPI.Core.IServices;
using ProductAPI.Extensions;
using ProductAPI.Repository;
using ProductAPI.Service;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOracle<ProductDbContext>(configuration.GetConnectionString("OracleConnection"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<ProductMemory>();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.LoadDataToMemory<ProductMemory, ProductDbContext>((productInMe, dbContext) =>
{
    new ProductMemorySeedAsync().SeedAsync(productInMe, dbContext).Wait();
});


app.Run();
