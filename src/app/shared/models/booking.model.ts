export interface CreateBookingDto {
  roomId: number;
  checkInDate: string;
  checkOutDate: string;
  numberOfRooms: number;
  selectedServices: BookingServiceItemDto[];
}

export interface BookingServiceItemDto {
  amenityId: number;
  quantity: number;
}

export interface BookingResponse {
  bookingId: number;
  roomType: string;
  imageUrl: string;
  checkInDate: string;
  checkOutDate: string;
  numberOfDays: number;
  numberOfRooms: number;
  status: string;
  bill: Bill;
}

export interface Bill {
  roomCharge: number;
  serviceCharge: number;
  demandSurcharge: number;
  seasonSurcharge: number;
  taxAmount: number;
  totalAmount: number;
  isPaid: boolean;
}