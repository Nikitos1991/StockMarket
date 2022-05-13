using Microsoft.AspNetCore.Mvc;
using StockMarket.Domain.Interfaces;

namespace StockMarket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IStocksRepository _stockRepository;

        public StocksController(ILogger logger, IStocksRepository stockRepository)
        {
            _logger = logger;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string id)
        {
            await _stockRepository.GetAsync(id);
            return new OkResult();
        }
    }
}