using Microsoft.AspNetCore.Mvc;
using StockMarket.Domain.Interfaces;

namespace StockMarket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IStockHistoryRepository _stockRepository;

        public StocksController(ILogger logger, IStockHistoryRepository stockRepository)
        {
            _logger = logger;
            _stockRepository = stockRepository;
        }

        [HttpGet("{symbol}/weekly-performance-report")]
        public async Task<IActionResult> GetWeeklyPerformanceReportAsync(string symbol)
        {
            var stockHistory = await _stockRepository.GetBySymbolAsync(symbol);
            return new OkObjectResult(stockHistory.GenerateWeeklyPerformanceReport());
        }

        [HttpGet("{symbol}/performance-comparison-report")]
        public async Task<IActionResult> GetAsync(string symbol, string comparisonSymbol)
        {
            var firstStockHistory = await _stockRepository.GetBySymbolAsync(symbol);
            var secondStockHistory = await _stockRepository.GetBySymbolAsync(comparisonSymbol);
            var firstStockWeeklyReport = firstStockHistory.GenerateWeeklyPerformanceReport();
            var secondStockWeeklyReport = secondStockHistory.GenerateWeeklyPerformanceReport();

            var performanceComparisonReport =firstStockWeeklyReport.Join(
                      secondStockWeeklyReport, 
                      first => first.Date, 
                      second => second.Date,
                      (first, second) => new
                      {
                          Date = first.Date,
                          FirstPerformance = first.Performance,
                          SecondPerformance = second.Performance
                      });


            return new OkObjectResult(performanceComparisonReport);
        }
    }
}