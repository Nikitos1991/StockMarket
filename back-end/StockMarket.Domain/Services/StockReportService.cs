using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using StockMarket.Domain.Interfaces;
using StockMarket.Domain.Models;
using StockMarket.Domain.Services.Interfaces;

namespace StockMarket.Domain.Services
{
    public class StockReportService: IStockReportService
    {
        private readonly ILogger _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IStocksRepository _stockRepository;
        public StockReportService(
            ILogger logger,
            IStocksRepository stockRepository,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _stockRepository = stockRepository;
            _memoryCache = memoryCache;
        }

        public  async Task<IEnumerable<StockPerformance>> GetWeeklyPerformanceAsync(string symbol)
        {
            _logger.LogInformation($"Get weekly performance report for {symbol}");
            var stockHistory = await _memoryCache.GetOrCreateAsync(symbol, x => _stockRepository.GetHystoryAsync(symbol, DomainConstants.DaysInAWeek));
            return stockHistory.GeneratePerformanceReport();
        }

        public async Task<IEnumerable<StockPerformanceComparison>> GetWeeklyPerformanceComparisonAsync(string symbol1, string symbol2)
        {
            var stockHistoryTasks = new List<Task<StockHistory>>()
            {
               _memoryCache.GetOrCreateAsync(symbol1, x => _stockRepository.GetHystoryAsync(symbol1, DomainConstants.DaysInAWeek)),
               _memoryCache.GetOrCreateAsync(symbol2, x => _stockRepository.GetHystoryAsync(symbol2, DomainConstants.DaysInAWeek)) 
            };

            var historyTasks = await Task.WhenAll(stockHistoryTasks);
            var firstWeeklyReport = historyTasks[0]?.GeneratePerformanceReport();
            var secondWeeklyReport = historyTasks[1]?.GeneratePerformanceReport();

            if (firstWeeklyReport == null || secondWeeklyReport == null)
                return Enumerable.Empty<StockPerformanceComparison>();


            return firstWeeklyReport.Join(
                      secondWeeklyReport,
                      first => first.Date,
                      second => second.Date,
                      (first, second) => new StockPerformanceComparison
                      {
                         
                          Date = first.Date,
                          FirstPerformance = first.Performance,
                          SecondPerformance = second.Performance
                      });
        }
    }
}
