import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = true;
  constructor(private _authService: AuthService) { }

  ngOnInit(): void {
    this.isLoggedIn = this._authService.isAuthenticated();
  }

  logout(): void {
    this._authService.logout();
  }
}
