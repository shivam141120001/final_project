import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth/auth.service';
import { IExecutive } from '../../interfaces/executive';
import { ManagerService } from '../../services/manager.service';

@Component({
  selector: 'app-create-executive',
  templateUrl: './create-executive.component.html',
  styleUrls: ['./create-executive.component.css'],
})
export class CreateExecutiveComponent implements OnInit {
  executives: IExecutive[] = [];

  constructor(private _service: ManagerService, private _authService: AuthService) {}

  ngOnInit(): void {
    if (!this._authService.isAuthenticated()) {
      this._authService.navigate(['/manager/login']);
      return;
    }
  }

  form = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(20),
    ]),
    emailId: new FormControl('', [Validators.required, Validators.email]),
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    locality: new FormControl('', [Validators.required]),
    contactNumber: new FormControl('', [
      Validators.required,
      Validators.pattern('^([6-9]{1}[0-9]{9})$'),
    ]),
  });
  get f() {
    return this.form.controls;
  }

  getAllExecutives(): void {
    this._service.getAllExecutives().subscribe(
      (value) => {
        console.log(value);
        this.executives = value;
      },
      (error) => console.error(error)
    );
  }

  createExecutive(): void {
    this._service.createExecutive(this.form.value).subscribe(
      (value) => {
        console.log(value);
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
