using HotelBookingAPI.Data;
using HotelBookingAPI.DTOs;
using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
        Task<RoomDto?> GetRoomByIdAsync(int id);
        Task<RoomDto> CreateRoomAsync(RoomCreateDto dto);
        Task<RoomDto?> UpdateRoomAsync(int id, RoomUpdateDto dto);
        Task<bool> DeleteRoomAsync(int id);
        Task<IEnumerable<RoomDto>> FilterRoomsAsync(string? roomType, decimal? maxPrice, bool? availableOnly);
    }

    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
        {
            try
            {
                var rooms = await _context.Rooms
                    .Where(r => r.IsActive)
                    .ToListAsync();
                return MapToDto(rooms);
            }
            catch
            {
                // Fallback if IsActive column doesn't exist
                var rooms = await _context.Rooms.ToListAsync();
                return MapToDto(rooms);
            }
        }

        public async Task<RoomDto?> GetRoomByIdAsync(int id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id && r.IsActive);
            return room == null ? null : MapToDto(room);
        }

        public async Task<RoomDto> CreateRoomAsync(RoomCreateDto dto)
        {
            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                RoomType = dto.RoomType,
                BasePrice = dto.BasePrice,
                CurrentPrice = dto.BasePrice,
                Capacity = dto.Capacity,
                Description = dto.Description,
                Amenities = dto.Amenities,
                ImageUrl = dto.ImageUrl,
                TotalRooms = dto.TotalRooms,
                Rating = dto.Rating
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return MapToDto(room);
        }

        public async Task<RoomDto?> UpdateRoomAsync(int id, RoomUpdateDto dto)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (room == null) return null;

            if (!string.IsNullOrEmpty(dto.RoomType))
                room.RoomType = dto.RoomType;
            if (dto.CurrentPrice.HasValue)
                room.CurrentPrice = dto.CurrentPrice.Value;
            if (dto.Capacity.HasValue)
                room.Capacity = dto.Capacity.Value;
            if (dto.Description != null)
                room.Description = dto.Description;
            if (dto.Amenities != null)
                room.Amenities = dto.Amenities;
            if (dto.TotalRooms.HasValue)
                room.TotalRooms = dto.TotalRooms.Value;

            room.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return MapToDto(room);
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (room == null) return false;

            room.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RoomDto>> FilterRoomsAsync(string? roomType, decimal? maxPrice, bool? availableOnly)
        {
            var query = _context.Rooms.Where(r => r.IsActive);

            if (!string.IsNullOrEmpty(roomType))
                query = query.Where(r => r.RoomType == roomType);

            if (maxPrice.HasValue)
                query = query.Where(r => r.CurrentPrice <= maxPrice.Value);

            if (availableOnly.HasValue && availableOnly.Value)
                query = query.Where(r => r.TotalRooms > 0);

            var rooms = await query.ToListAsync();
            return MapToDto(rooms);
        }

        private RoomDto MapToDto(Room room)
        {
            return new RoomDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                RoomType = room.RoomType,
                BasePrice = room.BasePrice,
                CurrentPrice = room.CurrentPrice,
                Capacity = room.Capacity,
                Description = room.Description,
                Amenities = room.Amenities,
                ImageUrl = room.ImageUrl,
                TotalRooms = room.TotalRooms,
                Rating = room.Rating,
                ReviewCount = room.ReviewCount
            };
        }

        private IEnumerable<RoomDto> MapToDto(IEnumerable<Room> rooms)
        {
            return rooms.Select(MapToDto);
        }
    }
}
