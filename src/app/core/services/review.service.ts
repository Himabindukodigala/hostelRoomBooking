import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReviewDto } from '../../shared/models/review.models';
import { CreateBookingDto } from '../../shared/models/booking.model';


@Injectable({ providedIn: 'root' })
export class ReviewService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api';

  createReview(reviewData: CreateBookingDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/reviews`, reviewData);
  }

  getRoomReviews(roomId: number): Observable<ReviewDto[]> {
    return this.http.get<ReviewDto[]>(`${this.apiUrl}/reviews/room/${roomId}`);
  }
}