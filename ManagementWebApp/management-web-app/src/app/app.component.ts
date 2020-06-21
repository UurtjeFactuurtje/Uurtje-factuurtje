import { Component } from '@angular/core';

import { AuthenticationService } from './services/authentication.service'
import { Router } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Uurtje Factuurtje management app';
  currentUser: any;

  constructor(private authenticationService: AuthenticationService,
    private router: Router) {
    this.authenticationService.userName.subscribe(x => this.currentUser = x);
  }

  logout() {
    this.authenticationService.signout();
    this.router.navigate(['/login']);
  }
}
