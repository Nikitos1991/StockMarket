using Microsoft.AspNetCore.Mvc;
using StockMarket.API.Mappers;
using StockMarket.Domain.Services.Interfaces;

namespace StockMarket.Controllers
{
    [ApiController]
    [Route("stock-reports")]
    public class StockReportsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IStockReportService _service;
        private readonly IStockReportsMapper _mapper;


        public StockReportsController(
            ILogger logger,
            IStockReportService service,
            IStockReportsMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("weekly-performance")]
        public async Task<IActionResult> GetWeeklyPerformanceReportAsync(string symbol)
        {
            var performanceReport = await _service.GetWeeklyPerformanceAsync(symbol);
            return new OkObjectResult(_mapper.MapStockPerformanceResponse(performanceReport));
        }

        [HttpGet("weekly-performance-comparison")]
        public async Task<IActionResult> GetWeeklyPerformanceComparisonReportAsync(string symbol1, string symbol2)
        {
            var comparisonReport = await _service.GetWeeklyPerformanceComparisonAsync(symbol1, symbol2);
            return new OkObjectResult(_mapper.MapStockPerformanceComparisonResponse(comparisonReport));
        }
    }
}