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
    }
}
