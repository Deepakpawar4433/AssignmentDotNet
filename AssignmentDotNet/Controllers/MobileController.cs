using AssignmentDotNet.DTOs;
using AssignmentDotNet.Service.MobileService;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly IMobileService _mobileService;
        public MobileController(IMobileService mobileService)
        {
            _mobileService = mobileService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMobiles()
        {
            var mobiles = await _mobileService.GetAllMobiles();
            return Ok(mobiles);
        }
        [HttpPost]
        public async Task<IActionResult> AddMobile([FromBody] MobileDto mobile)
        {
            if (mobile == null)
            {
                return BadRequest("Invalid mobile data.");
            }
            await _mobileService.AddMobile(mobile);
            return Ok("Mobile added successfully.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMobileById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid mobile ID.");
            }

            var mobile = await _mobileService.GetMobileById(id);
            if (mobile == null)
            {
                return NotFound("Mobile not found.");
            }

            return Ok(mobile);
        }
        //[HttpPut]
        //public async Task<IActionResult> UpdateMobile(MobileDto mobileDto)
        //{
        //    if (mobileDto == null || mobileDto.Id <= 0)
        //    {
        //        return BadRequest("Invalid mobile data.");
        //    }

        //    string result = await _mobileService.UpdateMobile(mobileDto);

        //    if (result == "Mobile not found.")
        //    {
        //        return NotFound(result);
        //    }

        //    return Ok(result);
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMobile(int id, [FromBody] MobileDto mobileDto)
        {
            if (id <= 0 || mobileDto == null || id != mobileDto.Id)
            {
                return BadRequest("Invalid mobile data or ID mismatch.");
            }

            string result = await _mobileService.UpdateMobileById(id, mobileDto);

            if (result == "Mobile not found.")
            {
                return NotFound(result);
            }

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMobile(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid mobile ID.");
            }
            await _mobileService.DeleteMobile(id);
            return Ok("Mobile deleted successfully.");
        }
        [HttpGet("best-price/{mobileId}")]
        public async Task<IActionResult> GetBestPrice(int mobileId)
        {
            if (mobileId <= 0)
                return BadRequest("Invalid Mobile ID.");

            decimal bestPrice = await _mobileService.GetBestPrice(mobileId);

            if (bestPrice == 0)
                return NotFound("No sales data available for this handset.");

            return Ok(new { MobileId = mobileId, BestPrice = bestPrice });
        }
    }
}
