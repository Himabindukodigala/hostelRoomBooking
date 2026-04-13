export interface Room {
  id: number;
  roomNumber: string;
  roomType: string;
  description: string;
  rating: number;
  imageUrl: string;
  totalRooms: number;
  availableRooms: number;
  basePrice: number;
  currentPrice: number;
  isAvailable: boolean;
  maxOccupancy: number;
  amenities: Amenity[];
}

export interface Amenity {
  id: number;
  name: string;
  description: string;
  price: number;
  category: string;
}

export interface PriceEstimateRequest {
  roomId: number;
  checkInDate: string;
  checkOutDate: string;
  numberOfRooms: number;
}

export interface PriceEstimateResponse {
  basePrice: number;
  demandSurchargePercent: number;
  seasonSurchargePercent: number;
  finalPricePerNight: number;
  totalEstimate: number;
  pricingNote: string;
}