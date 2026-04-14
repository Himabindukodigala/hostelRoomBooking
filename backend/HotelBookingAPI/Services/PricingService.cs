using HotelBookingAPI.DTOs;

namespace HotelBookingAPI.Services
{
    public interface IPricingService
    {
        Task<PriceEstimateDto> GetPriceEstimateAsync(PriceEstimateRequestDto request);
        decimal CalculateDemandSurcharge(DateTime checkInDate, decimal basePrice);
        decimal CalculateSeasonSurcharge(DateTime checkInDate, decimal basePrice);
        decimal CalculateTax(decimal subtotal);
    }

    public class PricingService : IPricingService
    {
        public async Task<PriceEstimateDto> GetPriceEstimateAsync(PriceEstimateRequestDto request)
        {
            var numberOfNights = (int)(request.CheckOutDate - request.CheckInDate).TotalDays;
            if (numberOfNights <= 0) numberOfNights = 1;

            var basePrice = 100; // Default base price
            var roomCharge = basePrice * numberOfNights * request.NumberOfRooms;

            var demandSurcharge = CalculateDemandSurcharge(request.CheckInDate, roomCharge);
            var seasonSurcharge = CalculateSeasonSurcharge(request.CheckInDate, roomCharge);
            var subtotal = roomCharge + demandSurcharge + seasonSurcharge;
            var taxAmount = CalculateTax(subtotal);
            var totalAmount = subtotal + taxAmount;

            return new PriceEstimateDto
            {
                BasePrice = basePrice,
                RoomCharge = roomCharge,
                DemandSurcharge = demandSurcharge,
                DemandSurchargePercent = (demandSurcharge / roomCharge) * 100,
                SeasonSurcharge = seasonSurcharge,
                SeasonSurchargePercent = (seasonSurcharge / roomCharge) * 100,
                TaxAmount = taxAmount,
                TotalAmount = totalAmount,
                NumberOfNights = numberOfNights,
                NumberOfRooms = request.NumberOfRooms
            };
        }

        public decimal CalculateDemandSurcharge(DateTime checkInDate, decimal basePrice)
        {
            var dayOfWeek = checkInDate.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Friday || dayOfWeek == DayOfWeek.Saturday)
                return basePrice * 0.15m;
            return 0;
        }

        public decimal CalculateSeasonSurcharge(DateTime checkInDate, decimal basePrice)
        {
            var month = checkInDate.Month;
            if (month >= 12 || month <= 2)
                return basePrice * 0.20m;
            return 0;
        }

        public decimal CalculateTax(decimal subtotal)
        {
            return subtotal * 0.10m;
        }
    }
}
