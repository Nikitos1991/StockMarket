using Microsoft.Extensions.Options;
using StockMarket.Domain.Interfaces;
using StockMarket.Domain.Models;
using StockMarket.Infrastructure.Models;
using System.Net.Http.Json;

namespace StockMarket.Infrastructure.Repositories
{
    public class StockHistoryRepository : IStockHistoryRepository
    {
        private readonly YahooFinanceApiOptions _options;
        private readonly HttpClient _httpClient;
        public StockHistoryRepository(HttpClient httpClient, IOptions<YahooFinanceApiOptions> options)
        {
            _options = options.Value;
            _httpClient = httpClient;
        }

        public async Task<StockHistory> GetBySymbolAsync(string symbol)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _options.YahooFinanceHost);
                _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _options.YahooFinanceApiKey);
                var stockHistoryUrl = new Uri($"https://{_options.YahooFinanceHost}/stock/v3/get-historical-data?symbol={symbol}");
                var historyResponse = await _httpClient.GetFromJsonAsync<StocksHistoryResponse>(stockHistoryUrl);
                return new StockHistory
                {
                    Symbol = symbol,
                    Prices = historyResponse?.Prices?.Select(p => new StockPrice
                    {
                        Price = p.Close,
                        Date = p.Date
                    })
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
