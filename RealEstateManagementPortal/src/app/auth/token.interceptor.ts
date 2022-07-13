import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable, throwError } from 'rxjs';
@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  private _omitCalls = ['login', 'managerLogin', 'executiveLogin'];
  private skipInterceptor: boolean = false;
  constructor(private _authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.skipInterceptor = false;
    this._omitCalls.forEach((path) => {
      if (request.url.includes(path)) {
        this.skipInterceptor = true;
      }
    });
    if (!this.skipInterceptor) {
      if (!this._authService.isAuthenticated()) {
        this._authService.logout();
        return throwError("Login Again");
      }
      const token = this._authService.getToken();
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
    } else console.log("Token is not attached");
    return next.handle(request);
  }

  handleRequestInterupt(error: any): any {
    console.error(error?.message);
  }
}
