import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule} from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './routes/app-routing.module';
import { ProjectsComponent } from './projects/projects.component';
import { LoginComponent } from './login/login.component';
import { FakeBackendProvider} from './helpers/fake-backend'
import { ErrorInterceptor } from './helpers/error.interceptor';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { TeamsComponent } from './teams/teams.component';
import { PeopleComponent } from './people/people.component';


@NgModule({
  declarations: [
    AppComponent,
    ProjectsComponent,
    LoginComponent,
    TeamsComponent,
    PeopleComponent
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
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    HttpClientModule,
    //Fake backend for testing the login functionality
    FakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
