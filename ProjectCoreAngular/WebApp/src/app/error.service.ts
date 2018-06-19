import { Injectable } from "@angular/core";
import { AuthenticationService } from "./authentication/authentication.service";
import { AlertService } from "./alert.service";
import { Observable } from "rxjs/Observable";

@Injectable()
export class ErrorService {

  constructor(private authenticationService: AuthenticationService, private alertService: AlertService) { }

  commonCatch(status) {
    if (status == 401)
      this.authenticationService.logout();
    else
      this.alertService.danger();
  }
}
