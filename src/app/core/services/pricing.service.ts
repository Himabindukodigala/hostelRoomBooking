import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PriceEstimateRequest, PriceEstimateResponse } from '../../shared/models/room.model';

@Injectable({ providedIn: 'root' })
export class PricingService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api';

  getPriceEstimate(request: PriceEstimateRequest): Observable<PriceEstimateResponse> {
    return this.http.post<PriceEstimateResponse>(`${this.apiUrl}/pricing/estimate`, request);
  }
}