namespace StockMarket.Domain.Interfaces
{
    public interface IStocksRepository
    {
        Task GetAsync(string id);
    }
}
