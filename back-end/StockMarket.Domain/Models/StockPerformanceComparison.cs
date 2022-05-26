namespace StockMarket.Domain.Models
{
    public class StockPerformanceComparison
    {
        public DateTime Date { get; set; }
        public decimal FirstPerformance { get; set; }
        public decimal SecondPerformance { get; set; }
    }
}
