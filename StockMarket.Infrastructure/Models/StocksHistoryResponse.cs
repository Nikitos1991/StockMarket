namespace StockMarket.Infrastructure.Models
{
    public class StocksHistoryResponse
    {
        public IEnumerable<StockPriceResponse> Prices { get; set; }
    }

    public class StockPriceResponse
    {
        public long Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal AdjClose { get; set; }
    }
}
