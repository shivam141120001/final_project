import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreatePropertyComponent } from './components/create-property/create-property.component';
import { CreateExecutiveComponent } from './components/create-executive/create-executive.component';
import { DetailPropertyComponent } from './components/detail-property/detail-property.component';
import { DetailCustomerComponent } from './components/detail-customer/detail-customer.component';
import { AssignExecutiveComponent } from './components/assign-executive/assign-executive.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ManagerService } from './services/manager.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ManagerLoginComponent } from './components/manager-login/manager-login.component';
import { ManagerHomeComponent } from './components/manager-home/manager-home.component';

@NgModule({
  declarations: [
    CreatePropertyComponent,
    CreateExecutiveComponent,
    DetailPropertyComponent,
    DetailCustomerComponent,
    AssignExecutiveComponent,
    ManagerLoginComponent,
    ManagerHomeComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'manager', component: ManagerHomeComponent },
      { path: 'manager/create-property', component: CreatePropertyComponent },
      { path: 'manager/create-executive', component: CreateExecutiveComponent },
      { path: 'manager/detail-property', component: DetailPropertyComponent },
      { path: 'manager/detail-customer', component: DetailCustomerComponent },
      { path: 'manager/assign-executive', component: AssignExecutiveComponent },
      { path: 'manager/login', component: ManagerLoginComponent },
    ]),
  ],
  providers: [ManagerService],
})
export class ManagerModule {}
