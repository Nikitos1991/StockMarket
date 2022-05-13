namespace StockMarket.Domain.Interfaces
{
    public interface IStockHistoryRepository
    {
        Task<StockHistory> GetBySymbolAsync(string symbol);
    }
}
