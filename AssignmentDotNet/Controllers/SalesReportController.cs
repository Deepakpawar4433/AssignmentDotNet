using AssignmentDotNet.Service.SalesReportService;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReportController : ControllerBase
    {
        private readonly ISalesReportService _salesReportService;

        public SalesReportController(ISalesReportService salesReportService)
        {
            _salesReportService = salesReportService;
        }

        [HttpGet("monthly-sales")]
        public async Task<IActionResult> GetMonthlySalesReport([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var report = await _salesReportService.GetMonthlySalesReport(fromDate, toDate);
            return Ok(report);
        }

        [HttpGet("brand-sales")]
        public async Task<IActionResult> GetBrandWiseSalesReport([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var report = await _salesReportService.GetBrandWiseSalesReport(fromDate, toDate);
            return Ok(report);
        }
        [HttpGet("profit-loss")]
        public async Task<IActionResult> GetProfitLossReport([FromQuery] DateTime currentFromDate, [FromQuery] DateTime currentToDate, [FromQuery] DateTime previousFromDate, [FromQuery] DateTime previousToDate)
        {
            var report = await _salesReportService.GetProfitLossReport(currentFromDate, currentToDate, previousFromDate, previousToDate);
            return Ok(report);
        }
    }
}
