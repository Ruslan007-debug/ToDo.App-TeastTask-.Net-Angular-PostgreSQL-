import { Component } from "@angular/core";
import { Router, RouterLink } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { AuthService } from "../../core/services/authService";
import { AuthRequest } from "../../models/authRequestModel";

@Component({
    selector: 'app-login',
    imports: [FormsModule, RouterLink],
    templateUrl: './login.html',
    styleUrls: ['./login.css']
})
export class Login{
    request: AuthRequest = { email: '', password: '' }; 
    errorMessage = '';

    constructor(
        private authService: AuthService,
        private router: Router
    ){}
    login(): void {
        this.authService.login(this.request).subscribe({ //підписується на Observable, який повертає метод login() сервісу AuthService з переданим параметром request типу AuthRequest
            next: () => { //якщо запит успішний, то виконується метод navigate() сервісу Router, який перенаправляє користувача на сторінку /tasks
                this.router.navigate(['/tasks']);
            },
            error: (error) => { //якщо запит неуспішний, то виводиться повідомлення про помилку, яка відображається на сторінці
                this.errorMessage = 'Login failed. Please check your credentials.';
            }
        });
    }
}