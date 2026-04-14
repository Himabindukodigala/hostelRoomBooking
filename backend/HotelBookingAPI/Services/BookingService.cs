using HotelBookingAPI.Data;
using HotelBookingAPI.DTOs;
using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Services
{
    public interface IBookingService
    {
        Task<BookingDto?> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId);
        Task<BookingResponseDto> CreateBookingAsync(int userId, BookingCreateDto dto);
        Task<bool> CancelBookingAsync(int bookingId);
        Task<bool> CheckoutAsync(int bookingId);
    }

    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDto?> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == id);
            return booking == null ? null : MapToDto(booking);
        }

        public async Task<IEnumerable<BookingDto>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();
            return bookings.Select(MapToDto);
        }

        public async Task<BookingResponseDto> CreateBookingAsync(int userId, BookingCreateDto dto)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == dto.RoomId);
            if (room == null || room.TotalRooms < dto.NumberOfRooms)
            {
                return new BookingResponseDto
                {
                    BookingId = 0,
                    Message = "Room not available"
                };
            }

            var booking = new Booking
            {
                UserId = userId,
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                NumberOfRooms = dto.NumberOfRooms,
                TotalPrice = 0,
                Status = "Pending"
            };

            _context.Bookings.Add(booking);
            room.TotalRooms -= dto.NumberOfRooms;

            if (dto.SelectedServices != null && dto.SelectedServices.Any())
            {
                foreach (var service in dto.SelectedServices)
                {
                    var amenity = await _context.Amenities.FirstOrDefaultAsync(a => a.Id == service.AmenityId);
                    if (amenity != null)
                    {
                        _context.BookingAmenities.Add(new BookingAmenity
                        {
                            Booking = booking,
                            AmenityId = service.AmenityId,
                            Quantity = service.Quantity,
                            Price = amenity.Price
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();

            return new BookingResponseDto
            {
                BookingId = booking.Id,
                Message = "Booking created successfully"
            };
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId);
            if (booking == null) return false;

            booking.Status = "Cancelled";
            booking.CancelledAt = DateTime.UtcNow;

            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == booking.RoomId);
            if (room != null)
                room.TotalRooms += booking.NumberOfRooms;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckoutAsync(int bookingId)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId);
            if (booking == null) return false;

            booking.Status = "Confirmed";
            await _context.SaveChangesAsync();
            return true;
        }

        private BookingDto MapToDto(Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                RoomId = booking.RoomId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                NumberOfRooms = booking.NumberOfRooms,
                TotalPrice = booking.TotalPrice,
                Status = booking.Status,
                BookedAt = booking.BookedAt
            };
        }
    }
}
