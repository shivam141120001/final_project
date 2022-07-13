import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-manager-home',
  templateUrl: './manager-home.component.html',
  styleUrls: ['./manager-home.component.css']
})
export class ManagerHomeComponent implements OnInit {

  routes: string[] = [];

  constructor(private _router: Router, private _authService: AuthService) { }

  ngOnInit(): void {
    if (!this._authService.isAuthenticated()) {
      this._authService.navigate(['/manager/login']);
      return;
    }
    this._getRoutes();
  }

  private _getRoutes(): void {
    for (let route of this._router.config) {
      if (!route.path?.startsWith('manager') || route.path?.startsWith('manager/login')) continue;
      this.routes.push(('/' + route.path) as string);
    }
  }
}
