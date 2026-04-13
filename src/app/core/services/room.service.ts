import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Room } from '../../shared/models/room.model';

@Injectable({ providedIn: 'root' })
export class RoomService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api';

  getAllRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(`${this.apiUrl}/rooms`);
  }

  getRoomById(id: number): Observable<Room> {
    return this.http.get<Room>(`${this.apiUrl}/rooms/${id}`);
  }

  getAvailableRooms(checkIn: string, checkOut: string): Observable<Room[]> {
    return this.http.get<Room[]>(`${this.apiUrl}/rooms/available`, {
      params: { checkIn, checkOut }
    });
  }
}