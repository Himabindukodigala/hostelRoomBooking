using HotelBookingAPI.DTOs;
using HotelBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricingController : ControllerBase
    {
        private readonly IPricingService _pricingService;

        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [HttpPost("estimate")]
        public async Task<IActionResult> GetPriceEstimate([FromBody] PriceEstimateRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estimate = await _pricingService.GetPriceEstimateAsync(request);
            return Ok(estimate);
        }
    }
}
