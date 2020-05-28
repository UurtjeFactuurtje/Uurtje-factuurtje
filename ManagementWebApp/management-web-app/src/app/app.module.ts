import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { HttpClientModule } from '@angular/common/http';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ReactiveFormsModule} from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './routes/app-routing.module';
import { ProjectsComponent } from './projects/projects.component';
import { LoginComponent } from './login/login.component';
import { FakeBackendProvider} from './helpers/fake-backend'

@NgModule({
  declarations: [
    AppComponent,
    ProjectsComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatGridListModule,
    HttpClientModule,
    MatDatepickerModule,
    ReactiveFormsModule
  ],
  providers: [
    //Fake backend for testing the login functionality
    FakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
