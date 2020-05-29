import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectsComponent } from '../projects/projects.component';
import { LoginComponent } from '../login/login.component';
import {AuthGuard} from '../helpers/auth.guard'

const routes: Routes = [
  { path: '', component: ProjectsComponent, canActivate:[AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: '**', component: ProjectsComponent, canActivate:[AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }