using HotelBookingAPI.Data;
using HotelBookingAPI.DTOs;
using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Services
{
    public interface IBillingService
    {
        Task<BillDto?> GetBillByBookingIdAsync(int bookingId);
        Task<BillDto> CreateBillAsync(int bookingId, Booking booking);
        Task<BillDto?> UpdateBillAsync(int billId, BillDto dto);
    }

    public class BillingService : IBillingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPricingService _pricingService;

        public BillingService(ApplicationDbContext context, IPricingService pricingService)
        {
            _context = context;
            _pricingService = pricingService;
        }

        public async Task<BillDto?> GetBillByBookingIdAsync(int bookingId)
        {
            var bill = await _context.Bills
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
            return bill == null ? null : MapToDto(bill);
        }

        public async Task<BillDto> CreateBillAsync(int bookingId, Booking booking)
        {
            var estimate = await _pricingService.GetPriceEstimateAsync(
                new PriceEstimateRequestDto
                {
                    RoomId = booking.RoomId,
                    CheckInDate = booking.CheckInDate,
                    CheckOutDate = booking.CheckOutDate,
                    NumberOfRooms = booking.NumberOfRooms
                }
            );

            var bill = new Bill
            {
                BookingId = bookingId,
                RoomCharge = estimate.RoomCharge,
                ServiceCharge = 0,
                DemandSurcharge = estimate.DemandSurcharge,
                SeasonSurcharge = estimate.SeasonSurcharge,
                TaxAmount = estimate.TaxAmount,
                TotalAmount = estimate.TotalAmount
            };

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return MapToDto(bill);
        }

        public async Task<BillDto?> UpdateBillAsync(int billId, BillDto dto)
        {
            var bill = await _context.Bills.FirstOrDefaultAsync(b => b.Id == billId);
            if (bill == null) return null;

            bill.ServiceCharge = dto.ServiceCharge;
            bill.TotalAmount = dto.TotalAmount;
            await _context.SaveChangesAsync();
            return MapToDto(bill);
        }

        private BillDto MapToDto(Bill bill)
        {
            return new BillDto
            {
                Id = bill.Id,
                BookingId = bill.BookingId,
                RoomCharge = bill.RoomCharge,
                ServiceCharge = bill.ServiceCharge,
                DemandSurcharge = bill.DemandSurcharge,
                SeasonSurcharge = bill.SeasonSurcharge,
                TaxAmount = bill.TaxAmount,
                TotalAmount = bill.TotalAmount,
                PaymentStatus = bill.PaymentStatus
            };
        }
    }
}
