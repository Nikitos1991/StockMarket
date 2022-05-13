using Microsoft.Extensions.Options;
using StockMarket.Domain.Interfaces;
using StockMarket.Infrastructure.Models;

namespace StockMarket.Infrastructure.Repositories
{
    public class StocksRepository : IStocksRepository
    {
        private readonly YahooFinanceApiOptions _options;
        private readonly HttpClient _httpClient;
        public StocksRepository(HttpClient httpClient, IOptions<YahooFinanceApiOptions> options)
        {
            _options = options.Value;
            _httpClient = httpClient;
        }

        public async Task GetAsync(string id)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://yh-finance.p.rapidapi.com/stock/v3/get-historical-data?symbol={id}"),
                    Headers = {
                                { "X-RapidAPI-Host", "yh-finance.p.rapidapi.com" },
                                { "X-RapidAPI-Key", "c76a3ad4cbmsh630765fb542e234p197221jsn734d371cd62c" },
                              },
                };
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
