import { Component } from '@angular/core';
import { RegisterModel } from '../../services/models/register.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerModel: RegisterModel = new RegisterModel();

  constructor(private router: Router) {}

  onSubmit() {
    console.log('Register data:', this.registerModel);
    // TODO: call API
  }

  goToLogin(event: Event) {
    event.preventDefault();
    this.router.navigate(['/login']);
  }
}