import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule, AuthGuard } from './app-routing.module';

import { FormsModule } from '@angular/forms';
import { LoginComponent } from './site/auth/login/login.component';
import { DashboardComponent } from './site/panel/dashboard/dashboard.component';
import { AuthLayoutComponent } from './core/layout/auth-layout/auth-layout.component';
import { PanelLayoutComponent } from './core/layout/panel-layout/panel-layout.component';
import { AppComponent } from './app.component';
import { GlobalInterceptor } from './core/services/global.interceptor';
import { WerahousesComponent } from './site/panel/werahouses/werahouses.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DashboardComponent,
    AuthLayoutComponent,
    PanelLayoutComponent,
    WerahousesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: GlobalInterceptor, multi: true } // Register the interceptor
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
