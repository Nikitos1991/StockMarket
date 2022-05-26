namespace StockMarket.Domain.Models
{
    public class StockPerformance
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public decimal Performance { get; set; }
    }
}
