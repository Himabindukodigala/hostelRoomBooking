import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LoginDto, RegisterDto, AuthResponse } from '../../shared/models/auth.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5282/api';
  
  private isLoggedInSubject = new BehaviorSubject<boolean>(this.hasToken());
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }

  login(loginData: LoginDto): Observable<AuthResponse> {
    return this.http.post<any>(`${this.apiUrl}/auth/login`, loginData).pipe(
      tap(response => {
        if (response.token) {
          localStorage.setItem('token', response.token);
          if (response.userName) {
            localStorage.setItem('userName', response.userName);
          }
          this.isLoggedInSubject.next(true);
        }
      })
    );
  }

  register(registerData: RegisterDto): Observable<AuthResponse> {
    return this.http.post<any>(`${this.apiUrl}/auth/register`, registerData).pipe(
      tap(response => {
        if (response.token) {
          localStorage.setItem('token', response.token);
          if (response.userName) {
            localStorage.setItem('userName', response.userName);
          }
          this.isLoggedInSubject.next(true);
        }
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    this.isLoggedInSubject.next(false);
  }

  getUserName(): string {
    return localStorage.getItem('userName') || '';
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}