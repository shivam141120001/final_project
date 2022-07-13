import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerModule } from './modules/customer/customer.module';
import { ExecutiveModule } from './modules/executive/executive.module';
import { ManagerModule } from './modules/manager/manager.module';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginProfilesComponent } from './components/login-profiles/login-profiles.component';
import { HomeModule } from './modules/home/home.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './auth/token.interceptor';
import { AuthService } from './auth/auth.service';

@NgModule({
  declarations: [AppComponent, NavbarComponent, LoginProfilesComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ManagerModule,
    CustomerModule,
    ExecutiveModule,
    HomeModule,
  ],
  bootstrap: [AppComponent],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    AuthService,
  ],
})
export class AppModule {}
