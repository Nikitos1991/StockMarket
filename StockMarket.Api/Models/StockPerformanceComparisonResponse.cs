namespace StockMarket.API.Models
{
    public class StockPerformanceComparisonResponse
    {
        public string? Date { get; set; }
        public decimal FirstPerformance { get; set; }
        public decimal SecondPerformance { get; set; }
    }
}
