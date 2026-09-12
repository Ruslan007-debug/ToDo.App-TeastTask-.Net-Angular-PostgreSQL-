import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { authGuard } from './core/guards/authGuard';
import { Register } from './pages/register/register';

export const routes: Routes = 
[
  { 
    path: 'login',
    component: Login
  },
  { 
    path: '',
    redirectTo: 'login',
    pathMatch: 'full' 
  },
  {
    path: 'register',
    component: Register
  }
];
