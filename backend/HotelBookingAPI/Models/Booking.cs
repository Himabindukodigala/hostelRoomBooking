namespace HotelBookingAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled, Completed
        public DateTime BookedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CancelledAt { get; set; }
        public string? Notes { get; set; }

        // Foreign keys
        public virtual User? User { get; set; }
        public virtual Room? Room { get; set; }

        // Navigation properties
        public virtual ICollection<Bill>? Bills { get; set; }
        public virtual ICollection<BookingAmenity>? BookingAmenities { get; set; }
    }
}
