import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule} from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ConfigService } from './shared/config.service';


import { AppComponent } from './app.component';
import { AppRoutingModule } from './routes/app-routing.module';
import { ProjectsComponent } from './projects/projects.component';
import { LoginComponent } from './login/login.component';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { TeamsComponent } from './teams/teams.component';
import { PeopleComponent } from './people/people.component';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';


@NgModule({
  declarations: [
    AppComponent,
    ProjectsComponent,
    LoginComponent,
    TeamsComponent,
    PeopleComponent,
    AuthCallbackComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatGridListModule,
    HttpClientModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatTableModule,
    MatSelectModule,
    BrowserAnimationsModule
  ],
  providers: [
    ConfigService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    HttpClientModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
