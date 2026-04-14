using HotelBookingAPI.Data;
using HotelBookingAPI.DTOs;
using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Services
{
    public interface IAmenityService
    {
        Task<IEnumerable<AmenityDto>> GetAllAmenitiesAsync();
        Task<AmenityDto?> GetAmenityByIdAsync(int id);
        Task<AmenityDto> CreateAmenityAsync(AmenityCreateDto dto);
        Task<bool> UpdateAmenityAsync(int id, AmenityCreateDto dto);
        Task<bool> DeleteAmenityAsync(int id);
    }

    public class AmenityService : IAmenityService
    {
        private readonly ApplicationDbContext _context;

        public AmenityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AmenityDto>> GetAllAmenitiesAsync()
        {
            var amenities = await _context.Amenities
                .Where(a => a.IsActive)
                .ToListAsync();
            return amenities.Select(MapToDto);
        }

        public async Task<AmenityDto?> GetAmenityByIdAsync(int id)
        {
            var amenity = await _context.Amenities
                .FirstOrDefaultAsync(a => a.Id == id && a.IsActive);
            return amenity == null ? null : MapToDto(amenity);
        }

        public async Task<AmenityDto> CreateAmenityAsync(AmenityCreateDto dto)
        {
            var amenity = new Amenity
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
            return MapToDto(amenity);
        }

        public async Task<bool> UpdateAmenityAsync(int id, AmenityCreateDto dto)
        {
            var amenity = await _context.Amenities.FirstOrDefaultAsync(a => a.Id == id);
            if (amenity == null) return false;

            amenity.Name = dto.Name;
            amenity.Description = dto.Description;
            amenity.Price = dto.Price;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAmenityAsync(int id)
        {
            var amenity = await _context.Amenities.FirstOrDefaultAsync(a => a.Id == id);
            if (amenity == null) return false;

            amenity.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        private AmenityDto MapToDto(Amenity amenity)
        {
            return new AmenityDto
            {
                Id = amenity.Id,
                Name = amenity.Name,
                Description = amenity.Description,
                Price = amenity.Price
            };
        }
    }
}
