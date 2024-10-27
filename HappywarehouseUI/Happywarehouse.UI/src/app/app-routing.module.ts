import { Injectable, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CanActivate } from '@angular/router';
import { AuthService } from './core/services/authService';
import { LoginComponent } from './site/auth/login/login.component';
import { DashboardComponent } from './site/panel/dashboard/dashboard.component';
import { AuthLayoutComponent } from './core/layout/auth-layout/auth-layout.component';
import { PanelLayoutComponent } from './core/layout/panel-layout/panel-layout.component';
import { WerahousesComponent } from './site/panel/werahouses/werahouses.component';
import { ItemsComponent } from './site/panel/items/items.component';
import { UsersComponent } from './site/panel/users/users.component';

// Custom guard to protect routes
@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService) {}

  canActivate(): boolean {
    return this.authService.isAuthenticated();
  }
}


const routes: Routes = [
  {
    path: '',
    redirectTo: '/login',
    pathMatch: 'full'
  },
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      {
        path: 'login',
        component: LoginComponent
      }
    ]
  },
  {
    path: '',
    component: PanelLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent
      },
      {
        path: 'werahouses',
        component: WerahousesComponent
      },
      {
        path: 'items',
        component: ItemsComponent
      },
      {
        path: 'users',
        component: UsersComponent
      }
    ]
  },
  {
    path: '**',
    redirectTo: '/login' // Redirect unknown routes
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
