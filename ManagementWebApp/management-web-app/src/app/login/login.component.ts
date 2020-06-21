
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService, private spinner: NgxSpinnerService) { }

  title = "Login";

  login() {
    this.spinner.show();
    this.authenticationService.login();
  }

  ngOnInit() {
  }
}
