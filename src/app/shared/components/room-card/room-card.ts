import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Room } from '../../models/room.model';

@Component({
  standalone: true,
  selector: 'app-room-card',
  imports: [CommonModule, RouterModule],
  templateUrl: './room-card.html',
  styleUrl: './room-card.css',
})
export class RoomCard  {
  @Input() room!: Room;

  getStars(rating: number): number[] {
    return Array(Math.floor(rating)).fill(0);
  }

  getEmptyStars(rating: number): number[] {
    return Array(5 - Math.floor(rating)).fill(0);
  }
}
