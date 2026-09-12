import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { authGuard } from './core/guards/authGuard';
import { Register } from './pages/register/register';
import { Tasks } from './pages/tasks/tasks';

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
  },
  {
    path: 'tasks',
    component: Tasks,
    canActivate: [authGuard]
  }
];
