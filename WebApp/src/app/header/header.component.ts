import { Component, OnInit } from "@angular/core"

import { AppService } from "../app.service";
import { Router } from "@angular/router";
import { AuthenticationService } from "../authentication/authentication.service";
import { Observable } from "rxjs/Observable";

@Component({
  selector: 'mt-header',
  templateUrl: 'header.component.html',
  styleUrls: ['header.component.css']
})
export class HeaderComponent implements OnInit {

  isLoggedIn$: Observable<boolean>;

  constructor(private appService: AppService, private router: Router, private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.isLoggedIn$ = this.authenticationService.isLoggedIn;
  }

  logout() {
    this.authenticationService.logout();
  }

}
