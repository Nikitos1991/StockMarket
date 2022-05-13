using Microsoft.AspNetCore.Mvc;
using StockMarket.Domain.Interfaces;

namespace StockMarket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IStockHistoryRepository _stockRepository;

        public StocksController(ILogger logger, IStockHistoryRepository stockRepository)
        {
            _logger = logger;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string symbol)
        {
            var stockHistory = await _stockRepository.GetBySymbolAsync(symbol);
            var performanceReport = stockHistory.GeneratePerformanceReport();
            return new OkResult();
        }
    }
}