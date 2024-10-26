import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'Token'; // Token key

  constructor() {}

  saveToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return token !== null && this.isTokenValid(token);
  }

  private isTokenValid(token: string): boolean {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const expiry = payload.exp * 1000;
    return Date.now() < expiry;
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }
}
