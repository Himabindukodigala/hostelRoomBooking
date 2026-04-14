import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { BookingService } from '../../core/services/booking.service';
import { BillService } from '../../core/services/bill.service';
import { AmenityService } from '../../core/services/amenity.service';
import { Bill, BookingResponse } from '../../shared/models/booking.model';
import { Amenity } from '../../shared/models/room.model';
import { StarRating } from '../../shared/components/star-rating/star-rating';

@Component({
  standalone: true,
  selector: 'app-dashboard',
  imports: [CommonModule, FormsModule, RouterModule, StarRating],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  private router = inject(Router);
  private bookingService = inject(BookingService);
  private billService = inject(BillService);
  private amenityService = inject(AmenityService);
  
  bookings: BookingResponse[] = [];
  amenities: (Amenity & { selected: boolean, quantity: number })[] = [];
  isLoading = false;
  showReviewPopup = false;
  selectedBooking: BookingResponse | null = null;
  reviewRating = 5;
  reviewComment = '';

  ngOnInit() {
    this.loadUserBookings();
    this.loadAmenities();
  }

  loadUserBookings() {
    this.isLoading = true;
    this.bookingService.getUserBookings().subscribe({
      next: (bookings) => {
        this.bookings = bookings;
        this.isLoading = false;
      },
      error: () => {
        alert('Failed to load bookings');
        this.isLoading = false;
      }
    });
  }

  loadAmenities() {
    this.amenityService.getAllAmenities().subscribe({
      next: (amenities) => {
        this.amenities = amenities.map(a => ({ ...a, selected: false, quantity: 1 }));
      },
      error: () => {
        console.error('Failed to load amenities');
      }
    });
  }

  getStatusClass(status: string): string {
    switch(status) {
      case 'Confirmed':
        return 'badge bg-success';
      case 'Pending':
        return 'badge bg-warning';
      case 'Cancelled':
        return 'badge bg-danger';
      default:
        return 'badge bg-secondary';
    }
  }

  openReview(booking: BookingResponse) {
    this.selectedBooking = booking;
    this.showReviewPopup = true;
  }

  closeReview() {
    this.showReviewPopup = false;
    this.selectedBooking = null;
    this.reviewRating = 5;
    this.reviewComment = '';
  }

  onRatingChange(rating: number) {
    this.reviewRating = rating;
  }

  submitReview() {
    if (this.selectedBooking) {
      console.log('Review submitted:', {
        bookingId: this.selectedBooking.bookingId,
        rating: this.reviewRating,
        comment: this.reviewComment
      });
      this.closeReview();
      alert('Review submitted successfully!');
    }
  }
}
