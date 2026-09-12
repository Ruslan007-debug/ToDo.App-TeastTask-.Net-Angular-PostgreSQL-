import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "../services/authService";

export const authGuard: CanActivateFn = () => { //створює функцію охоронця маршруту, яка перевіряє, чи користувач авторизований
    const authService = inject(AuthService); //отримує екземпляр сервісу AuthService, який містить методи для роботи з токенами авторизації
    const router = inject(Router); //отримує екземпляр сервісу Router, який використовується для перенаправлення користувача на сторінку входу, якщо він не авторизований
 
    if (authService.isAuthenticated()) {
        return true;
  }
    return router.createUrlTree(['/login']); //якщо токен авторизації відсутній, то перенаправляє користувача на сторінку входу
}
