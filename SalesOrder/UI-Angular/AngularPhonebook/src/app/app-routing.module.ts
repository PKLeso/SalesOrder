import { Component, NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Auth/guards/auth-guard.service';
import { LoginComponent } from './Auth/login/login.component';
import { PhonebookComponent } from './phonebook/phonebook.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'phonebook', component:PhonebookComponent, canActivate: [AuthGuard] }
]

@NgModule({
  declarations: [],
  imports: [
    [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules})]
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }
