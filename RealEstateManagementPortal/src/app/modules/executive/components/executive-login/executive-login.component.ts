import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth/auth.service';
import { ILoginResponse } from 'src/app/interfaces/login-response';
import { ILoginUser } from 'src/app/interfaces/login-user';
import { Role } from 'src/app/interfaces/role';
import { ExecutiveService } from '../../services/executive.service';

@Component({
  selector: 'app-executive-login',
  templateUrl: './executive-login.component.html',
  styleUrls: ['./executive-login.component.css']
})
export class ExecutiveLoginComponent implements OnInit {

  constructor(private _service: ExecutiveService, private _authService: AuthService) { }
  
  ngOnInit(): void {
    if (!this._authService.isAuthenticated()) {
      this._authService.navigate(['/executive/login']);
      return;
    }
  }

  form= new FormGroup( {
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    role:new FormControl(Role.Executive,[Validators.required])
  });

  get f(){
    return this.form.controls;
  }

  loginExecutive():void{
    this._service.loginExecutive(this.form.value as ILoginUser).subscribe(
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
    this._authService.navigate(['/executive']);
  }
}
