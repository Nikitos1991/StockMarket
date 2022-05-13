using StockMarket.Domain.Interfaces;
using StockMarket.Infrastructure.Models;
using StockMarket.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IStockHistoryRepository, StockHistoryRepository>();
// builder.Services.AddOptions<YahooFinanceApiOptions>();
builder.Services.AddSingleton<ILogger>(logger => logger.GetRequiredService<ILoggerFactory>()
                         .CreateLogger("StockMarket"));

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

app.Run();
