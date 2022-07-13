import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { IProperty } from '../../interfaces/property';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-search-properties',
  templateUrl: './search-properties.component.html',
  styleUrls: ['./search-properties.component.css'],
})
export class SearchPropertiesComponent implements OnInit {
  constructor(private _service: CustomerService, private _authService: AuthService) {}

  public properties: IProperty[] = [];

  ngOnInit(): void {
    if (!this._authService.isAuthenticated()) {
      this._authService.navigate(['customer/login']);
      return;
    }
    this.getProperties();
  }

  getProperties(): void {
    this._service.getProperties().subscribe(
      (value) => {
        this.properties = value;
      },
      (error) => console.error(error)
    );
  }
}
