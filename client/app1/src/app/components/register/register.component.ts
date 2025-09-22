import { Component } from '@angular/core';
import { RegisterModel } from '../../services/models/register.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerModel: RegisterModel = new RegisterModel();

  constructor(private router: Router, private authService: AuthService) {}

  onSubmit() {
    console.log('Register data:', this.registerModel);
    
    this.authService.register(this.registerModel).subscribe({
        next: () => this.router.navigate(['/login']),
        error: (err) => console.error('Sign-up failed:', err)
      });
  }

  goToLogin(event: Event) {
    event.preventDefault();
    this.router.navigate(['/login']);
  }
}