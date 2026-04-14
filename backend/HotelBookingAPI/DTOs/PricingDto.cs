namespace HotelBookingAPI.DTOs
{
    public class PriceEstimateDto
    {
        public decimal BasePrice { get; set; }
        public decimal RoomCharge { get; set; }
        public decimal DemandSurcharge { get; set; }
        public decimal DemandSurchargePercent { get; set; }
        public decimal SeasonSurcharge { get; set; }
        public decimal SeasonSurchargePercent { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int NumberOfNights { get; set; }
        public int NumberOfRooms { get; set; }
    }

    public class PriceEstimateRequestDto
    {
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfRooms { get; set; }
    }
}
