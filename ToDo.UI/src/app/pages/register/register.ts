import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { AuthService } from '../../core/services/authService';
import { AuthRequest } from '../../models/authRequestModel';

@Component({
    selector: 'app-register',
    imports: [FormsModule, RouterLink],
    templateUrl: './register.html',
    styleUrls: ['./register.css']
})
export class Register {
    request: AuthRequest = { email: '', password: '' }; 
    errorMessage = '';

    constructor(
        private authService: AuthService,
        private router: Router
    ){}

    register(): void {
        this.authService.register(this.request).subscribe({
            next: () => {
                this.router.navigate(['/login']);
            },
            error: () => {
                this.errorMessage = 'Registration failed. Email may already be in use.';
            }
        });
    }
}