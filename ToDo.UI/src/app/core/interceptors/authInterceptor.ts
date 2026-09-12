import { HttpInterceptor, HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { AuthService } from "../services/authService";

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {  //створює інтерцептор, який перехоплює всі HTTP запити і додає до них токен авторизації, якщо він є в локальному сховищі браузера
    const authService = inject(AuthService); //отримує екземпляр сервісу AuthService, який містить методи для роботи з токенами авторизації
    const accessToken = authService.getAccessToken(); //отримує токен авторизації з локального сховища браузера за допомогою методу getAccessToken() сервісу AuthService

    if (accessToken) { //якщо токен авторизації є, то створює новий запит з доданим заголовком Authorization, який містить токен авторизації
        const authReq = req.clone({ //копіює поточний запит бо HttpRequest є immutable, і додає токен авторизайції
            setHeaders: {
                Authorization: `Bearer ${accessToken}`
            }
        });
        return next(authReq); //передає новий запит з доданим заголовком Authorization далі по ланцюжку інтерцепторів
    }
    return next(req);//якщо токен авторизації відсутній, то передає оригінальний запит далі по ланцюжку інтерцепторів

}