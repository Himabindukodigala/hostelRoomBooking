import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingService } from '../../core/services/booking.service';
import { BillService } from '../../core/services/bill.service';
import { AmenityService } from '../../core/services/amenity.service';
import { Bill, BookingResponse } from '../../shared/models/booking.model';
import { Amenity } from '../../shared/models/room.model';

@Component({
  standalone: true,
  selector: 'app-payment',
  imports: [CommonModule, FormsModule],
  templateUrl: './payment.html',
  styleUrl: './payment.css',
})
export class Payment  implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private bookingService = inject(BookingService);
  private billService = inject(BillService);
  private amenityService = inject(AmenityService);
  
  bookingId!: number;
  booking: BookingResponse | null = null;
  bill: Bill | null = null;
  amenities: (Amenity & { selected: boolean, quantity: number })[] = [];
  isLoading = false;
  showSuccess = false;

  ngOnInit() {
    this.bookingId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadBookingDetails();
    this.loadAmenities();
  }

  loadBookingDetails() {
    this.bookingService.getBookingById(this.bookingId).subscribe({
      next: (booking) => {
        this.booking = booking;
        this.loadBill();
      },
      error: () => {
        alert('Failed to load booking details');
      }
    });
  }

  loadBill() {
    this.billService.getBill(this.bookingId).subscribe({
      next: (bill) => {
        this.bill = bill;
      },
      error: () => {
        console.error('Failed to load bill');
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

  updateBill() {
    if (!this.bill) return;
    
    const selectedServices = this.amenities.filter(a => a.selected);
    const serviceTotal = selectedServices.reduce((sum, s) => sum + (s.price * s.quantity), 0);
    
    this.bill.serviceCharge = serviceTotal;
    this.bill.totalAmount = this.bill.roomCharge + this.bill.serviceCharge + 
                           this.bill.demandSurcharge + this.bill.seasonSurcharge + 
                           this.bill.taxAmount;
  }

  getServiceTotal(): number {
    return this.amenities.filter(a => a.selected).reduce((sum, s) => sum + (s.price * s.quantity), 0);
  }

  checkout() {
    this.isLoading = true;
    
    // Update booking with selected services
    const selectedServices = this.amenities.filter(a => a.selected).map(a => ({
      amenityId: a.id,
      quantity: a.quantity
    }));

    this.bookingService.checkout(this.bookingId).subscribe({
      next: () => {
        this.showSuccess = true;
        setTimeout(() => {
          this.router.navigate(['/dashboard']);
        }, 2000);
      },
      error: () => {
        alert('Checkout failed. Please try again.');
        this.isLoading = false;
      }
    });
  }
}
