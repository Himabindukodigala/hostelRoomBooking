import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bill } from '../../shared/models/booking.model';

@Injectable({ providedIn: 'root' })
export class BillService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5282/api';

  getBill(bookingId: number): Observable<Bill> {
    return this.http.get<Bill>(`${this.apiUrl}/bills/${bookingId}`);
  }
}