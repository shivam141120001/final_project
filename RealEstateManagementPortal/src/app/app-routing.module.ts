import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './modules/home/home.component';
import { LoginProfilesComponent } from './components/login-profiles/login-profiles.component';


const routes: Routes = [
  {
    path:'',
    component:HomeComponent,
  },
  {
    path:'login-profiles',
    component: LoginProfilesComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
