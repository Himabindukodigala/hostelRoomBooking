import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  standalone: true,
  selector: 'app-register',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
   private authService = inject(AuthService);
  private router = inject(Router);
  
  registerData = { fullName: '', email: '', password: '' };
  isLoading = false;
  errorMessage = '';

  onSubmit() {
    this.isLoading = true;
    this.errorMessage = '';
    
    this.authService.register(this.registerData).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Registration failed. Please try again.';
        this.isLoading = false;
      }
    });
  }
}
