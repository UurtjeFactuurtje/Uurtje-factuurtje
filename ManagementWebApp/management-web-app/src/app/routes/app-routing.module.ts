import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectsComponent } from '../projects/projects.component';
import { LoginComponent } from '../login/login.component';

const routes: Routes = [
  { path: '', component: ProjectsComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', component: ProjectsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }