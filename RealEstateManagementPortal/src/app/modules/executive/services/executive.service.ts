import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { ILoginResponse } from 'src/app/interfaces/login-response';
import { ILoginUser } from 'src/app/interfaces/login-user';
import { ICustomer } from '../../customer/interfaces/customer';
import { IExecutive } from '../interfaces/executive';

@Injectable({
  providedIn: 'root'
})
export class ExecutiveService {
  private _baseUrl: string = 'https://localhost:44348/api/Manager/';
  
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
    return throwError(errMsg);
  }

  getExecutiveById( executiveId : any) : Observable<IExecutive> {
    return this._http
    .get<IExecutive>(this._baseUrl + "getExecutiveById?executiveId="+executiveId)
    .pipe<IExecutive>(catchError(this.errorHandler));
  }

  getCustomersAssignedToExecutive( executiveId : any) : Observable<ICustomer[]> {
    return this._http
    .get<ICustomer[]>(this._baseUrl + "getAllCustomersByExecutive?executiveId="+executiveId)
    .pipe<ICustomer[]>(catchError(this.errorHandler));
  }

  loginExecutive(executive: ILoginUser): Observable<ILoginResponse> {
    return this._http
      .post<ILoginResponse>(this._baseUrl + "executiveLogin", executive)
      .pipe<ILoginResponse>(catchError(this.errorHandler));
  }
}

