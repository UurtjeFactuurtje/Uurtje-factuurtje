import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectsComponent } from '../projects/projects.component';
import { LoginComponent } from '../login/login.component';
import { AuthGuard } from '../helpers/auth.guard'
import { TeamsComponent } from '../teams/teams.component';
import { PeopleComponent } from '../people/people.component';
import { AuthCallbackComponent } from '../auth-callback/auth-callback.component';

const routes: Routes = [
  { path: 'Projects', component: ProjectsComponent, canActivate: [AuthGuard] },
  { path: 'Teams', component: TeamsComponent, canActivate: [AuthGuard] },
  { path: 'People', component: PeopleComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'auth-callback', component: AuthCallbackComponent },
  { path: '**', component: ProjectsComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
