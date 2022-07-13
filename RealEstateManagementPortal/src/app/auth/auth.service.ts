import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {
  private _TOKEN = 'Token';

  constructor(private _router: Router) {}

  public setToken(value: any): void {
    sessionStorage.setItem(this._TOKEN, value);
  }

  public getToken(): any {
    return sessionStorage.getItem(this._TOKEN);
  }

  public clearToken(): void {
    sessionStorage.removeItem(this._TOKEN);
  }

  private isTokenExpired(): boolean {
    const jwtToken: string = this.getToken();
    if (!jwtToken) return true;
    const tokenData: any = JSON.parse(atob(jwtToken.split('.')[1]));
    return Date.now() > (tokenData?.exp || 0) * 1000;
  }

  public isAuthenticated(): boolean {
    return !this.isTokenExpired();
  }

  public logout(): void {
    this.clearToken();
    this._router.navigate(['/']);
    window.location.reload();
  }

  public navigate(route: any[]) {
    this._router.navigate(route);
  }
}
