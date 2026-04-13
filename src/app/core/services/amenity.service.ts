import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Amenity } from '../../shared/models/room.model';

@Injectable({ providedIn: 'root' })
export class AmenityService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api';

  getAllAmenities(): Observable<Amenity[]> {
    return this.http.get<Amenity[]>(`${this.apiUrl}/amenities`);
  }

  getRoomAmenities(roomId: number): Observable<Amenity[]> {
    return this.http.get<Amenity[]>(`${this.apiUrl}/amenities/room/${roomId}`);
  }
}