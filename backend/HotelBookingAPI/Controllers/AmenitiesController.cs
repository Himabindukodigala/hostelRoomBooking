using HotelBookingAPI.DTOs;
using HotelBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenityService _amenityService;

        public AmenitiesController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAmenities()
        {
            var amenities = await _amenityService.GetAllAmenitiesAsync();
            return Ok(amenities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAmenityById(int id)
        {
            var amenity = await _amenityService.GetAmenityByIdAsync(id);
            if (amenity == null)
                return NotFound(new { message = "Amenity not found" });

            return Ok(amenity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAmenity([FromBody] AmenityCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var amenity = await _amenityService.CreateAmenityAsync(dto);
            return CreatedAtAction(nameof(GetAmenityById), new { id = amenity.Id }, amenity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAmenity(int id, [FromBody] AmenityCreateDto dto)
        {
            var success = await _amenityService.UpdateAmenityAsync(id, dto);
            if (!success)
                return NotFound(new { message = "Amenity not found" });

            return Ok(new { message = "Amenity updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            var success = await _amenityService.DeleteAmenityAsync(id);
            if (!success)
                return NotFound(new { message = "Amenity not found" });

            return NoContent();
        }
    }
}
