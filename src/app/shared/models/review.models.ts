export interface CreateReviewDto {
  bookingId: number;
  roomId: number;
  rating: number;
  comment: string;
}

export interface ReviewDto {
  userName: string;
  rating: number;
  comment: string;
  createdAt: string;
}