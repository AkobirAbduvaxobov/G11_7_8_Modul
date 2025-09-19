import { Component } from '@angular/core';
import { LoginModel } from '../../services/models/login.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginModel: LoginModel = new LoginModel();

  constructor(private router: Router) {}

  onSubmit() {
    console.log('Login data:', this.loginModel);
    // TODO: call API
  }

  goToRegister() {
    this.router.navigate(['/register']);
  }
}