using StockMarket.Domain.Extensions;
using StockMarket.Domain.Models;

namespace StockMarket
{
    public class StockHistory
    {
        public IEnumerable<StockPrice>? Prices { get; set; }

        public IEnumerable<StockPerformance> GeneratePerformanceReport()
        {
            if (Prices == null)
                return Enumerable.Empty<StockPerformance>();

            var firstDay = Prices.First();
            return Prices.Select(p => new StockPerformance
            {
                Date = p.Date.ToUnixTimestamp().Date,
                Performance = ((p.Price / firstDay.Price)-1) * 100,
                Price = p.Price
            }
            );
        }
    }
}