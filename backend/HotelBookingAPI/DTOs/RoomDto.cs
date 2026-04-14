namespace HotelBookingAPI.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public string? Amenities { get; set; }
        public string? ImageUrl { get; set; }
        public int TotalRooms { get; set; }
        public float Rating { get; set; }
        public int ReviewCount { get; set; }
    }

    public class RoomCreateDto
    {
        public string RoomNumber { get; set; } = string.Empty;
        public string RoomType { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public string? Amenities { get; set; }
        public string? ImageUrl { get; set; }
        public int TotalRooms { get; set; }
        public float Rating { get; set; }
    }

    public class RoomUpdateDto
    {
        public string? RoomType { get; set; }
        public decimal? CurrentPrice { get; set; }
        public int? Capacity { get; set; }
        public string? Description { get; set; }
        public string? Amenities { get; set; }
        public int? TotalRooms { get; set; }
    }
}
