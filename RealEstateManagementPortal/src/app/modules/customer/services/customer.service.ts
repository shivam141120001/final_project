import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { IProperty } from '../interfaces/property';
import { ILoginUser } from 'src/app/interfaces/login-user';
import { ILoginResponse } from 'src/app/interfaces/login-response';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  //   loginCustomer(value: Partial<{ username: string | null; password: string | null; role: string | null; }>) {
  //     throw new Error('Method not implemented.');
  //   }
  private _baseUrl: string = 'https://localhost:44366/api/Customer/';

  constructor(private _http: HttpClient) {}

  private errorHandler(error: HttpErrorResponse) {
    let errMsg = '';
    switch (error.status) {
      case 0:
        errMsg = 'Server Down';
        break;
      default:
        errMsg = error.message || 'Server Error';
        break;
    }
    console.error(error);
    return throwError(errMsg);
  }

  getProperties(): Observable<IProperty[]> {
    return this._http
      .get<IProperty[]>(this._baseUrl + 'getProperties')
      .pipe<IProperty[]>(catchError(this.errorHandler));
  }

  //Second function:--
  createCustomer(customer: any): Observable<any> {
    return this._http
      .post(this._baseUrl + 'createCustomer', customer)
      .pipe(catchError(this.errorHandler));
  }

  loginCustomer(customer: ILoginUser): Observable<ILoginResponse> {
    return this._http
      .post<ILoginResponse>(this._baseUrl + 'login', customer)
      .pipe<ILoginResponse>(catchError(this.errorHandler));
  }
}
