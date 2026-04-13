import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RoomService } from '../../core/services/room.service';
import { PricingService } from '../../core/services/pricing.service';
import { BookingService } from '../../core/services/booking.service';
import { AuthService } from '../../core/services/auth.service';
import { PriceEstimateResponse, Room } from '../../shared/models/room.model';
import { StarRating } from '../../shared/components/star-rating/star-rating';

@Component({
  standalone: true,
  selector: 'app-room-details',
  imports: [CommonModule, FormsModule, StarRating],
  templateUrl: './room-details.html',
  styleUrl: './room-details.css',
})
export class RoomDetails  implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private roomService = inject(RoomService);
  private pricingService = inject(PricingService);
  private bookingService = inject(BookingService);
  private authService = inject(AuthService);
  
  room: Room | null = null;
  checkInDate = '';
  checkOutDate = '';
  numberOfRooms = 1;
  priceEstimate: PriceEstimateResponse | null = null;
  showAlert = false;
  alertMessage = '';

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.roomService.getRoomById(id).subscribe({
      next: (room) => {
        this.room = room;
        this.setDefaultDates();
      },
      error: () => {
        this.showError('Room not found!');
      }
    });
  }

  setDefaultDates() {
    const today = new Date();
    this.checkInDate = today.toISOString().split('T')[0];
    const tomorrow = new Date(today);
    tomorrow.setDate(tomorrow.getDate() + 1);
    this.checkOutDate = tomorrow.toISOString().split('T')[0];
    this.updatePrice();
  }

  updatePrice() {
    if (this.room && this.checkInDate && this.checkOutDate && this.numberOfRooms > 0) {
      this.pricingService.getPriceEstimate({
        roomId: this.room.id,
        checkInDate: this.checkInDate,
        checkOutDate: this.checkOutDate,
        numberOfRooms: this.numberOfRooms
      }).subscribe({
        next: (estimate) => {
          this.priceEstimate = estimate;
        },
        error: () => {
          this.showError('Failed to calculate price');
        }
      });
    }
  }

  incrementRooms() {
    if (this.room && this.numberOfRooms < this.room.availableRooms) {
      this.numberOfRooms++;
      this.updatePrice();
    }
  }

  decrementRooms() {
    if (this.numberOfRooms > 1) {
      this.numberOfRooms--;
      this.updatePrice();
    }
  }

  canBook(): boolean {
    return !!this.authService.getToken() && this.room !== null && 
           this.numberOfRooms > 0 && this.numberOfRooms <= (this.room?.availableRooms || 0);
  }

  onRatingChange(rating: number) {
    console.log('Rating changed to:', rating);
  }

  proceedToPayment() {
    if (!this.authService.getToken()) {
      this.router.navigate(['/login']);
      return;
    }

    if (this.room && this.priceEstimate) {
      this.bookingService.createBooking({
        roomId: this.room.id,
        checkInDate: this.checkInDate,
        checkOutDate: this.checkOutDate,
        numberOfRooms: this.numberOfRooms,
        selectedServices: []
      }).subscribe({
        next: (response) => {
          this.router.navigate(['/payment', response.bookingId]);
        },
        error: () => {
          this.showError('Failed to create booking. Please try again.');
        }
      });
    }
  }

  private showError(message: string) {
    this.alertMessage = message;
    this.showAlert = true;
    setTimeout(() => {
      this.showAlert = false;
    }, 3000);
  }
}
