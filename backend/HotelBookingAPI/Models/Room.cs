namespace HotelBookingAPI.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty; // Single, Double, Suite, etc.
        public decimal BasePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public string? Amenities { get; set; } // JSON string or comma-separated
        public string? ImageUrl { get; set; }
        public int TotalRooms { get; set; }
        public bool IsActive { get; set; } = true;
        public float Rating { get; set; } = 0f;
        public int ReviewCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Booking>? Bookings { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
