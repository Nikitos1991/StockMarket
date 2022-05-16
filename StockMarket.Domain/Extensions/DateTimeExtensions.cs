namespace StockMarket.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUnixTimestamp(this long timestamp)
        {
            return (new DateTime(1970, 1, 1)).AddSeconds(timestamp).ToUniversalTime();
        }
    }
}
