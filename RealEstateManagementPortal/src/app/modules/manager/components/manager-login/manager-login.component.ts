import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth/auth.service';
import { ILoginResponse } from 'src/app/interfaces/login-response';
import { ILoginUser } from 'src/app/interfaces/login-user';
import { Role } from 'src/app/interfaces/role';
import { ManagerService } from '../../services/manager.service';

@Component({
  selector: 'app-manager-login',
  templateUrl: './manager-login.component.html',
  styleUrls: ['./manager-login.component.css']
})
export class ManagerLoginComponent implements OnInit {

  constructor(
    private _service: ManagerService,
    private _authService: AuthService
  ) { }

  ngOnInit(): void {
    if (!this._authService.isAuthenticated()) {
      this._authService.navigate(['/manager/login']);
      return;
    }
  }

  form= new FormGroup( {
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    role: new FormControl(Role.Manager,[Validators.required])
  });

  get f(){
    return this.form.controls;
  }

  loginManager():void{
    this._service.loginManager(this.form.value as ILoginUser).subscribe(
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
    this._authService.navigate(['/manager']);
  }

}
