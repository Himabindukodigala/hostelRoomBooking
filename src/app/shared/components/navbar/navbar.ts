import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  standalone: true,
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit, OnDestroy {
  private authService = inject(AuthService);
  private router = inject(Router);
  private authSubscription!: Subscription;
  
  isLoggedIn = false;
  userName = '';

  ngOnInit() {
    this.authSubscription = this.authService.isLoggedIn$.subscribe(status => {
      this.isLoggedIn = status;
      if (status) {
        this.userName = this.authService.getUserName();
      }
    });
  }

  ngOnDestroy() {
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }

  logout() {
    this.authService.logout(); //auth ervices vifies token and after verification it removes the token in localstroage
    this.router.navigate(['/login']);
  }
}

