using StockMarket.API.Models;
using StockMarket.Domain.Models;

namespace StockMarket.API.Mappers
{
    public interface IStockReportsMapper
    {
        IEnumerable<StockPerformanceResponse> MapStockPerformanceResponse(IEnumerable<StockPerformance> stockPerformances);
        public IEnumerable<StockPerformanceComparisonResponse> MapStockPerformanceComparisonResponse(IEnumerable<StockPerformanceComparison> performanceComparisons);
    }
}
