using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StockMarket.Domain.Interfaces;
using StockMarket.Domain.Models;
using StockMarket.Infrastructure.Models;

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
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://yh-finance.p.rapidapi.com/stock/v3/get-historical-data?symbol={symbol}"),
                    Headers = {
                                { "X-RapidAPI-Host", "yh-finance.p.rapidapi.com" },
                                { "X-RapidAPI-Key", "c76a3ad4cbmsh630765fb542e234p197221jsn734d371cd62c" },
                              },
                };
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var historyResponse = JsonConvert.DeserializeObject<StocksHistoryResponse>(await response.Content.ReadAsStringAsync());

                return new StockHistory
                {
                    Symbol = symbol,
                    Prices = historyResponse.Prices.Select(p => new StockPrice { Price = p.Close, Date = p.Date })
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
