namespace HotelBookingAPI.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime BookedAt { get; set; }
    }

    public class BookingCreateDto
    {
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfRooms { get; set; }
        public List<BookingAmenityDto>? SelectedServices { get; set; }
    }

    public class BookingResponseDto
    {
        public int BookingId { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class BookingAmenityDto
    {
        public int AmenityId { get; set; }
        public int Quantity { get; set; }
    }
}
