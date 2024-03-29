using StockMarket.API.Mappers;
using StockMarket.Domain.Interfaces;
using StockMarket.Domain.Services;
using StockMarket.Domain.Services.Interfaces;
using StockMarket.Infrastructure.Models;
using StockMarket.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IStocksRepository, StocksRepository>();
builder.Services.AddScoped<IStockReportsMapper, StockReportsMapper>();
builder.Services.AddScoped<IStockReportService, StockReportService>();
builder.Services.AddMemoryCache();
builder.Services.Configure<StockMarketApiOptions>(builder.Configuration);
builder.Services.AddSingleton<ILogger>(logger => logger.GetRequiredService<ILoggerFactory>()
                         .CreateLogger("StockMarket"));


var app = builder.Build();

// Configure the HTTP request pipeline.
// To be accessible on all environments if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
