import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { RoomService } from '../../core/services/room.service';
import { Room } from '../../shared/models/room.model';
import { RoomCard } from '../../shared/components/room-card/room-card';

@Component({
  standalone: true,
  selector: 'app-rooms',
  imports: [CommonModule, FormsModule, RouterModule, RoomCard],
  templateUrl: './rooms.html',
  styleUrl: './rooms.css',
})
export class Rooms implements OnInit {
  private roomService = inject(RoomService);
  
  rooms: Room[] = [];
  filteredRooms: Room[] = [];
  filterType = '';
  maxPrice = 10000;
  maxPossiblePrice = 10000;
  showAvailableOnly = false;

  ngOnInit() {
    this.roomService.getAllRooms().subscribe(rooms => {
      this.rooms = rooms;
      this.maxPossiblePrice = Math.max(...rooms.map(r => r.currentPrice));
      this.maxPrice = this.maxPossiblePrice;
      this.applyFilters();
    });
  }

  applyFilters() {
    this.filteredRooms = this.rooms.filter(room => {
      if (this.filterType && room.roomType !== this.filterType) return false;
      if (room.currentPrice > this.maxPrice) return false;
      if (this.showAvailableOnly && room.availableRooms === 0) return false;
      return true;
    });
  }
}
