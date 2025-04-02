using AssignmentDotNet.DTOs;
using AssignmentDotNet.Service.DiscountService;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscounts()
        {
            var discounts = await _discountService.GetAllDiscounts();
            return Ok(discounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid discount ID.");

            var discount = await _discountService.GetDiscountById(id);
            if (discount == null)
                return NotFound("Discount not found.");

            return Ok(discount);
        }

        [HttpPost]
        public async Task<IActionResult> AdddDiscount([FromBody] DiscountDto discount)
        {
            if (discount == null)
                return BadRequest("Invalid discount data.");

            string result = await _discountService.AddDiscount(discount);
            if (result == "MobileId does not exist in the Mobile table.")
                return BadRequest("MobileId does not exist in the Mobile table.");

            return Ok("Discount added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, [FromBody] DiscountDto discountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }
            if (id <= 0 || discountDto == null || id != discountDto.Id)
            {
                return BadRequest("Invalid discount data or ID mismatch.");
            }

            string result = await _discountService.UpdateDiscount(id, discountDto);

            if (result == "Discount not found.")
            {
                return NotFound(result);
            }
            if (result == "MobileId does not exist in the Mobile table.")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid discount ID.");

            await _discountService.DeleteDiscount(id);
            return Ok("Discount deleted successfully.");
        }
    }
}
