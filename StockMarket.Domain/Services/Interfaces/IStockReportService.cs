using StockMarket.Domain.Models;

namespace StockMarket.Domain.Services.Interfaces
{
    public interface IStockReportService
    {
        Task<IEnumerable<StockPerformance>> GetWeeklyPerformanceAsync(string symbol);
        Task<IEnumerable<StockPerformanceComparison>> GetWeeklyPerformanceComparisonAsync(string symbol1, string symbol2);
    }
}
