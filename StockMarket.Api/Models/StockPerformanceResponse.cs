namespace StockMarket.API.Models
{
    public class StockPerformanceResponse
    {
        public string Date { get; set; }
        public decimal Price { get; set; }
        public decimal Performance { get; set; }
    }
}
