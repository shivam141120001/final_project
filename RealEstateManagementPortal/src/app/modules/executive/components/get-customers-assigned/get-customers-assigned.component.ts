import { Component, OnInit } from '@angular/core';
import { ICustomer } from '../../interfaces/customer';
import { ExecutiveService } from '../../services/executive.service';

@Component({
  selector: 'app-get-customers-assigned',
  templateUrl: './get-customers-assigned.component.html',
  styleUrls: ['./get-customers-assigned.component.css'],
})
export class GetCustomersAssignedComponent implements OnInit {
  private _loggedInExecutiveId: number = 2;
  public customers: ICustomer[] = [];

  constructor(private _service: ExecutiveService) {}

  ngOnInit(): void {
    this.getCustomersAssignedToExecutive();
  }

  getCustomersAssignedToExecutive(): void {
    this._service
      .getCustomersAssignedToExecutive(this._loggedInExecutiveId)
      .subscribe(
        (value) => {
          console.log(value);
          this.customers = value;
        },
        (error) => console.error(error)
      );
  }
}
