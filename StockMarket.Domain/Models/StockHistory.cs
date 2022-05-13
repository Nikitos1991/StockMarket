using StockMarket.Domain.Models;

namespace StockMarket
{
    public class StockHistory
    {
        public string Symbol { get; set; }
        public IEnumerable<StockPrice> Prices { get; set; }

        public IEnumerable<StockPerformance> GeneratePerformanceReport()
        {
            var lastWeekPrices = Prices.OrderBy(x => x.Date).TakeLast(7);
            var firstDay = lastWeekPrices.First();
            return lastWeekPrices.Select(p => new StockPerformance
            {
                Date = ParseUnixTimestamp(p.Date),
                Performance = (p.Price / firstDay.Price) * 100,
                Price = p.Price
            }
            );
        }

        public static DateTime ParseUnixTimestamp(long timestamp)
        {
            return (new DateTime(1970, 1, 1)).AddSeconds(timestamp).ToUniversalTime();
        }
    }
}