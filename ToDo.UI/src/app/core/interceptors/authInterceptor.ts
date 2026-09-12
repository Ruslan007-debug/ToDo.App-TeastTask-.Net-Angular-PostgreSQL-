import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { Router} from "@angular/router";
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from "../services/authService";

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {  //створює інтерцептор, який перехоплює всі HTTP запити і додає до них токен авторизації, якщо він є в локальному сховищі браузера
    const authService = inject(AuthService); //отримує екземпляр сервісу AuthService, який містить методи для роботи з токенами авторизації
     const router = inject(Router);
    const accessToken = authService.getAccessToken(); //отримує токен авторизації з локального сховища браузера за допомогою методу getAccessToken() сервісу AuthService

    let authReq = req;

    if (accessToken) { //якщо токен авторизації є, то створює новий запит з доданим заголовком Authorization, який містить токен авторизації
        authReq = req.clone({ //копіює поточний запит бо HttpRequest є immutable, і додає токен авторизайції
            setHeaders: {
                Authorization: `Bearer ${accessToken}`
            }
        });
    }

    return next(authReq).pipe(
        catchError((error: HttpErrorResponse) => {
            const isAuthRequest =
                req.url.includes('/login') || 
                req.url.includes('/register') || 
                req.url.includes('/refresh');
            if (error.status === 401 && !isAuthRequest) {
                const refreshToken = authService.getRefreshToken();
                if (!refreshToken) {
                    authService.clearTokens();
                    router.navigate(['/login']);

                    return throwError(() => error);
                }
                return authService.refreshToken().pipe(
                    switchMap((tokens) => {
                        const retryRequest = req.clone({
                            setHeaders: {
                                Authorization: `Bearer ${tokens.accessToken}`
                            }
                        });
                        return next(retryRequest);
                    }),
                    catchError((refreshError) => {
                        authService.clearTokens();
                        router.navigate(['/login']);
                        return throwError(() => refreshError);
                    })
                );
            }
            return throwError(() => error);
        })
    );
};