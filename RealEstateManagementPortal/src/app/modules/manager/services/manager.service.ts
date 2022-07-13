import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { IExecutive } from '../interfaces/executive';
import { ICustomer } from '../interfaces/customer';
import { IProperty } from '../interfaces/property';
import { ILoginUser } from 'src/app/interfaces/login-user';
import { ILoginResponse } from 'src/app/interfaces/login-response';

@Injectable({
  providedIn: 'root',
})
export class ManagerService {
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
    console.error(error);
    return throwError(errMsg);
  }

  getCustomerDetails(customerId: any): Observable<ICustomer> {
    return this._http
      .get<ICustomer>(
        this._baseUrl + 'getCustomersById?customerId=' + customerId
      )
      .pipe<ICustomer>(catchError(this.errorHandler));
  }

  getAllExecutives(): Observable<IExecutive[]> {
    return this._http
      .get<IExecutive[]>(this._baseUrl + 'getAllExecutives')
      .pipe<IExecutive[]>(catchError(this.errorHandler));
  }

  getPropertyByLocality(locality: string): Observable<IProperty[]> {
    return this._http
      .get<any>(this._baseUrl + 'propertyByLocality?Locality=' + locality)
      .pipe<any>(catchError(this.errorHandler));
  }

  getPropertyByType(propertyType: string): Observable<IProperty[]> {
    return this._http
      .get<any>(this._baseUrl + 'propertyByType?Type=' + propertyType)
      .pipe<any>(catchError(this.errorHandler));
  }

  createExecutive(executive: any): Observable<any> {
    return this._http
      .post(this._baseUrl + 'createExecutive', executive)
      .pipe(catchError(this.errorHandler));
  }

  createProperty(property: any): Observable<any> {
    return this._http
      .post(this._baseUrl + 'createProperty', property)
      .pipe(catchError(this.errorHandler));
  }

  assignExecutiveToCustomer(
    customerId: number,
    executiveId: number
  ): Observable<any> {
    console.log(customerId, executiveId);
    return this._http
      .put(this._baseUrl + 'assignExecutive', { customerId, executiveId })
      .pipe(catchError(this.errorHandler));
  }

  getAllCustomers(): Observable<ICustomer[]> {
    return this._http
      .get<ICustomer[]>(this._baseUrl + 'getAllCustomers')
      .pipe<ICustomer[]>(catchError(this.errorHandler));
  }

  loginManager(manager: ILoginUser): Observable<ILoginResponse> {
    return this._http
      .post<ILoginResponse>(this._baseUrl + 'managerLogin', manager)
      .pipe<ILoginResponse>(catchError(this.errorHandler));
  }
}
