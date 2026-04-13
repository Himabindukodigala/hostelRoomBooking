import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { RoomService } from '../../core/services/room.service';
import { Room } from '../../shared/models/room.model';
import { RoomCard } from '../../shared/components/room-card/room-card';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [CommonModule, RouterModule, RoomCard],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  private roomService = inject(RoomService);
  featuredRooms: Room[] = [];

  ngOnInit() {
    this.roomService.getAllRooms().subscribe(rooms => {
      this.featuredRooms = rooms.slice(0, 3);
    });
  }}
