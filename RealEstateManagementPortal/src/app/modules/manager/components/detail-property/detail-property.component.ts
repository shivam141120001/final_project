import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth/auth.service';
import { IProperty } from '../../interfaces/property';
import { ManagerService } from '../../services/manager.service';

@Component({
  selector: 'app-detail-property',
  templateUrl: './detail-property.component.html',
  styleUrls: ['./detail-property.component.css'],
})
export class DetailPropertyComponent implements OnInit {
  properties: IProperty[] = [];

  constructor(private _service: ManagerService, private _authService: AuthService) {}

  ngOnInit(): void {
    if (!this._authService.isAuthenticated()) {
      this._authService.navigate(['/manager/login']);
      return;
    }
  }

  form = new FormGroup({
    locality: new FormControl('', [Validators.required]),
  });
  get f() {
    return this.form.controls;
  }

  getPropertyDetails(): void {
    this._service
      .getPropertyByLocality(this.form.value.locality as string)
      .subscribe(
        (value) => {
          console.log(value);
          this.properties = value;
        },
        (error) => console.error(error)
      );
  }
}
