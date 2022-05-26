namespace StockMarket.Domain.Interfaces
{
    public interface IStocksRepository
    {
        Task<StockHistory> GetHystoryAsync(string symbol, int lastDaysPeriod);
    }
}
