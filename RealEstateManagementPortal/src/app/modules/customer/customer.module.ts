import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterDetailsComponent } from './components/register-details/register-details.component';
import { RouterModule } from '@angular/router';
import { SearchPropertiesComponent } from './components/search-properties/search-properties.component';
import { HttpClientModule } from '@angular/common/http';
import { CustomerService } from './services/customer.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CustomerLoginComponent } from './components/customer-login/customer-login.component';
import { AuthService } from 'src/app/auth/auth.service';

@NgModule({
  declarations: [RegisterDetailsComponent, SearchPropertiesComponent, CustomerLoginComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {
        path: 'customer/register-details',
        component: RegisterDetailsComponent,
      },
      {
        path: 'customer/search-properties',
        component: SearchPropertiesComponent
      },
      {
        path: 'customer/login',
        component: CustomerLoginComponent,
      },
    ]),
  ],
  providers: [CustomerService],
})
export class CustomerModule {}
