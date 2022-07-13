import { Component, OnInit,ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-register-details',
  templateUrl: './register-details.component.html',
  styleUrls: ['./register-details.component.css'],
})
export class RegisterDetailsComponent implements OnInit {
  constructor(private _service: CustomerService) {}

  ngOnInit(): void {}

  form = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(20),
    ]),
    emailId: new FormControl('', [Validators.required, Validators.email]),
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    address: new FormControl('', [Validators.required]),
    contactNumber: new FormControl('', [
      Validators.required,
      Validators.pattern('^([6-9]{1}[0-9]{9})$'),
    ]),
  });
  get f() {
    return this.form.controls;
  }



  createCustomer(): void {
    console.log(this.form.value);

    this._service.createCustomer(this.form.value).subscribe(
      (value) => {
        console.log(value);
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
