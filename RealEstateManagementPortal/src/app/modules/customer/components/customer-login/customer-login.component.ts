import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth/auth.service';
import { ILoginResponse } from 'src/app/interfaces/login-response';
import { ILoginUser } from 'src/app/interfaces/login-user';
import { Role } from 'src/app/interfaces/role';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-customer-login',
  templateUrl: './customer-login.component.html',
  styleUrls: ['./customer-login.component.css'],
})
export class CustomerLoginComponent implements OnInit {
  constructor(
    private _service: CustomerService,
    private _authService: AuthService
  ) {}

  ngOnInit(): void {
    if (this._authService.isAuthenticated()) {
      this._authService.navigate(['/customer/search-properties']);
      return;
    }
  }

  form = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    role: new FormControl(Role.Customer, [Validators.required]),
  });

  get f() {
    return this.form.controls;
  }

  loginCustomer(): void {
    this._service.loginCustomer(this.form.value as ILoginUser).subscribe(
      (value) => {
        this.handleLoginSuccess(value);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  handleLoginSuccess(value: ILoginResponse): void {
    this._authService.setToken(value.token);
    this._authService.navigate(['/customer/search-properties']);
  }
}
