namespace HotelBookingAPI.Models
{
    public class BookingAmenity
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int AmenityId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }

        // Foreign keys
        public virtual Booking? Booking { get; set; }
        public virtual Amenity? Amenity { get; set; }
    }
}
