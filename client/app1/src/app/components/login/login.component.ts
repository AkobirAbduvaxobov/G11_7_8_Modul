import { Component } from '@angular/core';
import { LoginModel } from '../../services/models/login.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginModel: LoginModel = new LoginModel();

  constructor(private router: Router, private authService: AuthService) {}

  onSubmit() {
    console.log('Login data:', this.loginModel);
    
    this.authService.login(this.loginModel).subscribe({
        next: () => this.router.navigate(['/login']),
        error: (err) => console.error('Sign-up failed:', err)
      });

  }

  goToRegister() {
    this.router.navigate(['/register']);
  }
}