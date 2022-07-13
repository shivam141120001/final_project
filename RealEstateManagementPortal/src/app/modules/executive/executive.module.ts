import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GetExecutiveDetailsComponent } from './components/get-executive-details/get-executive-details.component';
import { GetCustomersAssignedComponent } from './components/get-customers-assigned/get-customers-assigned.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ExecutiveLoginComponent } from './components/executive-login/executive-login.component';
import { ExecutiveHomeComponent } from './components/executive-home/executive-home.component';


@NgModule({
  declarations: [GetExecutiveDetailsComponent, GetCustomersAssignedComponent, ExecutiveLoginComponent, ExecutiveHomeComponent ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'executive', component: ExecutiveHomeComponent },
      {
        path: 'executive/get-executive-details',
        component: GetExecutiveDetailsComponent,
      },
      {
        path: 'executive/get-customers-assigned',
        component: GetCustomersAssignedComponent,
      },
      {
        path: 'executive/login',
        component: ExecutiveLoginComponent,
      },
    ]),
  ],
})
export class ExecutiveModule {}
