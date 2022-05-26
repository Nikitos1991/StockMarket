using StockMarket.API.Models;
using StockMarket.Domain.Models;

namespace StockMarket.API.Mappers
{
    public class StockReportsMapper : IStockReportsMapper
    {
        public IEnumerable<StockPerformanceResponse> MapStockPerformanceResponse(IEnumerable<StockPerformance> stockPerformances)
        {
            return stockPerformances.Select(sp => new StockPerformanceResponse
            {
                Date = sp.Date.ToShortDateString(),
                Performance = sp.Performance,
                Price = sp.Price
            });
        }

        public IEnumerable<StockPerformanceComparisonResponse> MapStockPerformanceComparisonResponse(IEnumerable<StockPerformanceComparison> performanceComparisons)
        {
            return performanceComparisons.Select(sp => new StockPerformanceComparisonResponse
            {
                Date = sp.Date.ToShortDateString(),
                FirstPerformance = sp.FirstPerformance,
                SecondPerformance = sp.SecondPerformance
            });
        }
    }
}
