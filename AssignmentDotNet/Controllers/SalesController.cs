using AssignmentDotNet.DTOs;
using AssignmentDotNet.Model;
using AssignmentDotNet.Service.SalesService;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _salesService.GetAllSales();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid sales ID.");

            var sale = await _salesService.GetSalesById(id);
            if (sale == null)
                return NotFound("Sales record not found.");

            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> AddSales([FromBody] SalesDto sales)
        {
            if (sales == null)
                return BadRequest("Invalid sales data.");

            string result = await _salesService.AddSales(sales);
            if (result == "MobileId does not exist in the Mobile table." || result == "DiscountId does not exist in the Discount table.")
                return BadRequest("MobileId does not exist in the Mobile table or DiscountId does not exist in the Discount table");
            return Ok("Sales record added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSales(int id, [FromBody] Sales sales)
        {
            if (id <= 0 || sales == null || id != sales.Id)
                return BadRequest("Invalid sales data or ID mismatch.");

            await _salesService.UpdateSales(sales);
            return Ok("Sales record updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSales(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid sales ID.");

            await _salesService.DeleteSales(id);
            return Ok("Sales record deleted successfully.");
        }
    }
}
