import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth/auth.service';
import { ManagerService } from '../../services/manager.service';

@Component({
  selector: 'app-create-property',
  templateUrl: './create-property.component.html',
  styleUrls: ['./create-property.component.css'],
})
export class CreatePropertyComponent implements OnInit {
  constructor(private _service: ManagerService, private _authService: AuthService) {}

  ngOnInit(): void {
    if (!this._authService.isAuthenticated()) {
      this._authService.navigate(['/manager/login']);
      return;
    }
  }

  form = new FormGroup({
    propertyType: new FormControl('', [Validators.required]),
    locality: new FormControl('', [Validators.required]),
    budget: new FormControl('', [Validators.required]),
  });
  get f() {
    return this.form.controls;
  }

  createProperty(): void {
    console.log(this.form.value);
    this._service.createProperty(this.form.value).subscribe(
      (value) => {
        console.log(value);
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
