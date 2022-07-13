import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ILoginUser } from 'src/app/interfaces/login-user';
import { Role } from 'src/app/interfaces/role';
import { IExecutive } from '../../interfaces/executive';
import { ExecutiveService } from '../../services/executive.service';


@Component({
  selector: 'app-get-executive-details',
  templateUrl: './get-executive-details.component.html',
  styleUrls: ['./get-executive-details.component.css'],
})
export class GetExecutiveDetailsComponent implements OnInit {
  public executive!: IExecutive;

  constructor(private _service: ExecutiveService) {}

  loginuser : ILoginUser={
    username:"",
    password:"",
    role: Role.Manager
  };

  ngOnInit(): void {}

  form = new FormGroup({
    executiveId: new FormControl('', [Validators.required]),
  });
  get f() {
    return this.form.controls;
  }

  getExecutiveDetails(): void {
    this._service.getExecutiveById(this.form.value?.executiveId).subscribe(
      (value) => {
        console.log(value);
        this.executive = value;
      },
      (error) => console.error(error)
    );
  }
}
