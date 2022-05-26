using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StockMarket.Domain.Interfaces;
using StockMarket.Domain.Models;
using StockMarket.Infrastructure.Models;
using System.Net.Http.Json;

namespace StockMarket.Infrastructure.Repositories
{
    public class StocksRepository : IStocksRepository
    {
        private readonly StockMarketApiOptions _options;
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        public StocksRepository(
            HttpClient httpClient,
            ILogger logger,
            IOptions<StockMarketApiOptions> options)
        {
            _options = options.Value;
            _httpClient = httpClient;
            _logger = logger;

        }

        public async Task<StockHistory> GetHystoryAsync(string symbol, int lastDaysPeriod)
        {
            try
            {
                /*selected api https://blog.api.rakuten.net/api-tutorial-yahoo-finance/*/
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _options.StockMarketHost);
                _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _options.StockMarketApiKey);
                var stockHistoryUrl = new Uri($"https://{_options.StockMarketHost}/stock/v3/get-historical-data?symbol={symbol}");
                var historyResponse = await _httpClient.GetFromJsonAsync<StocksHistoryResponse>(stockHistoryUrl);
                return new StockHistory
                {
                    Prices = historyResponse?.Prices?.Select(p => new StockPrice
                    {
                        Price = p.Close,
                        Date = p.Date
                    }).OrderBy(x => x.Date).TakeLast(lastDaysPeriod)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to obtain stock history");
                throw;
            }
        }
    }
}
