import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth/auth.service';
import { ICustomer } from '../../interfaces/customer';
import { IExecutive } from '../../interfaces/executive';
import { ManagerService } from '../../services/manager.service';

@Component({
  selector: 'app-assign-executive',
  templateUrl: './assign-executive.component.html',
  styleUrls: ['./assign-executive.component.css'],
})
export class AssignExecutiveComponent implements OnInit {
  constructor(private _service: ManagerService, private _authService: AuthService) {}

  ngOnInit(): void {
    if (!this._authService.isAuthenticated()) {
      this._authService.navigate(['/manager/login']);
      return;
    }
    this.getAllCustomers();
    this.getAllExecutives();
  }

  executives: IExecutive[] = [];
  customers: ICustomer[] = [];

  form = new FormGroup({
    executive: new FormControl(null, [Validators.required]),
    customer: new FormControl(null, [Validators.required]),
  });

  compareExecutive(e1: IExecutive, e2: IExecutive): boolean {
    return e1 && e2 ? e1.executiveId === e2.executiveId : e1 === e2;
  }

  compareCustomer(c1: ICustomer, c2: ICustomer): boolean {
    return c1 && c2 ? c1.executiveId === c1.executiveId : c1 === c2;
  }

  getAllExecutives(): void {
    this._service.getAllExecutives().subscribe(
      (value) => {
        console.log(value);
        this.executives = value;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getAllCustomers(): void {
    this._service.getAllCustomers().subscribe(
      (value) => {
        console.log(value);
        this.customers = value;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  assignExecutiveToCustomer(): void {
    this._service
      .assignExecutiveToCustomer(
        (this.form.value.customer as unknown as ICustomer)?.customerId,
        (this.form.value.executive as unknown as ICustomer)?.executiveId
      )
      .subscribe(
        (value) => {
          console.log(value, 'assigned');
        },
        (error) => {
          console.error(error);
        }
      );
  }
}
