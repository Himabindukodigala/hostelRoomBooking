import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-star-rating',
  imports: [CommonModule],
  templateUrl: './star-rating.html',
  styleUrl: './star-rating.css',
})
export class StarRating {@Input() rating: number = 0;
  @Input() readonly: boolean = false;
  @Output() ratingChange = new EventEmitter<number>();
  
  stars = [1, 2, 3, 4, 5];

  rate(value: number) {
    if (!this.readonly) {
      this.rating = value;
      this.ratingChange.emit(this.rating);
    }
  }}
