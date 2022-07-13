import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth/auth.service';
import { ICustomer } from '../../interfaces/customer';
import { ManagerService } from '../../services/manager.service';

@Component({
  selector: 'app-detail-customer',
  templateUrl: './detail-customer.component.html',
  styleUrls: ['./detail-customer.component.css'],
})
export class DetailCustomerComponent implements OnInit {
  public customer!: ICustomer;

  constructor(private _service: ManagerService, private _authService: AuthService) {}

  ngOnInit(): void {
    if (!this._authService.isAuthenticated()) {
      this._authService.navigate(['/manager/login']);
      return;
    }
  }

  form = new FormGroup({
    customerId: new FormControl('', [Validators.required]),
  });
  get f() {
    return this.form.controls;
  }

  getCustomerDetails(): void {
    this._service.getCustomerDetails(this.form.value?.customerId).subscribe(
      (value) => {
        console.log(value);
        this.customer = value;
      },
      (error) => console.error(error)
    );
  }
}
