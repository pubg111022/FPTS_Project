using b2.Core.Databases;
using b2.Core.IRepository;
using b2.Core.IServices;
using b2.Core.Repository;
using b2.Services;
using Customer_OrderAPI.Core.Databases;
using Customer_OrderAPI.Core.Databases.InMemory;
using Customer_OrderAPI.Extensions;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOracle<AppDbContext>(configuration.GetConnectionString("OracleConnection"));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddSingleton<CustomerMemory>();
builder.Services.AddSingleton<OrderMemory>();
builder.Services.AddSingleton<OrderItemMemory>();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.LoadDataToMemory<CustomerMemory, AppDbContext>((data, dbContext) =>
{
    new CustomerMemorySeedAsync().SeedAsync(data, dbContext).Wait();
});
app.LoadDataToMemory<OrderMemory, AppDbContext>((data, dbContext) =>
{
    new OrderMemorySeedAsync().SeedAsync(data, dbContext).Wait();
});
app.LoadDataToMemory<OrderItemMemory, AppDbContext>((data, dbContext) =>
{
    new OrderItemMemorySeedAsync().SeedAsync(data, dbContext).Wait();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
