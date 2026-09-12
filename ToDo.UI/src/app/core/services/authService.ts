import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { AuthRequest } from '../../models/authRequestModel';
import { TokenResponse } from '../../models/tokenResponseModel';
import { UserModel } from '../../models/userModel';
import {tap} from 'rxjs/operators';

@Injectable({      //decorator that marks a class as available to be provided and injected as a dependency.
  providedIn: 'root'    //один глобальний екземпляр сервісу, який буде доступний у всьому додатку, типу синглтон
})
export class AuthService 
    {
        private readonly apiUrl = 'https://localhost:7165/api/auth';

        constructor(private http: HttpClient) {}

        login(request: AuthRequest): Observable<TokenResponse> //метод логін з параметром реквест типу AuthRequest, який повертає Observable(дані які приходять пізніше) типу TokenResponse    
            {
                return this.http.post<TokenResponse>(`${this.apiUrl}/login`, request).pipe( //повертажає Observable типу TokenResponse, який отримується після відправки HTTP POST запиту на вказаний URL з переданими даними (request)
                    tap(tokens => {
                        this.saveTokens(tokens); //зберігає токени в локальному сховищі браузера
                    })
                );
            }

        register(request: AuthRequest): Observable<UserModel> //метод реєстрації з параметром реквест типу AuthRequest, який повертає Observable типу UserModel
            {
                return this.http.post<UserModel>(`${this.apiUrl}/register`, request); /*метод post відправляє HTTP POST запит на вказаний URL з переданими даними (request) і очікує отримати відповідь
                                                                                         у вигляді об'єкта UserModel */
            }

        saveTokens(token: TokenResponse): void //метод збереження токена в локальному сховищі браузера
            {
                localStorage.setItem('accessToken', token.accessToken); //зберігає токен в локальному сховищі браузера під ключем 'accessToken'
                localStorage.setItem('refreshToken', token.refreshToken); //зберігає токен в локальному сховищі браузера під ключем 'refreshToken'
            }

        getAccessToken(): string | null //метод отримання токена з локального сховища браузера
            {
                return localStorage.getItem('accessToken'); //повертає токен з локального сховища браузера під ключем 'accessToken'
            }

        getRefreshToken(): string | null //метод отримання токена з локального сховища браузера
            {
                return localStorage.getItem('refreshToken'); //повертає токен з локального сховища браузера під ключем 'refreshToken'
            }
        
        clearTokens(): void //метод видалення токена з локального сховища браузера
            {
                localStorage.removeItem('accessToken'); //видаляє токен з локального сховища браузера під ключем 'accessToken'
                localStorage.removeItem('refreshToken'); //видаляє токен з локального сховища браузера під ключем 'refreshToken'
            }
        
        logout(): Observable<void>
            {
                return this.http.post<void>(`${this.apiUrl}/logout`, {}).pipe(
                    tap(() => {
                        this.clearTokens(); //видаляє токени з локального сховища браузера
                    })
                );
            } //метод виходу з системи
    }