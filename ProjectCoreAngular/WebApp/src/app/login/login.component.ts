import { Component, OnInit } from "@angular/core"
import { ActivatedRoute, Router } from "@angular/router";
import { AuthenticationService } from "../authentication/authentication.service";
import { ErrorService } from "../error.service";
import { NgForm } from "@angular/forms";

class ModelLogin {
  Usuario: string;
  Senha: string;
}

@Component({
  selector: 'mt-login',
  templateUrl: 'login.component.html',
  styleUrls: ['login.component.css'],
  providers: [ModelLogin]
})
export class LoginComponent implements OnInit {

  constructor(private model: ModelLogin, private route: ActivatedRoute, private router: Router, private authenticationService: AuthenticationService, private errorService: ErrorService) { }

  ngOnInit() {
  }

  login(myForm: NgForm) {
    if (myForm.form.valid) {
      this.authenticationService.login(this.model)
        .subscribe(
          response => {

          },
          error => {
            this.errorService.commonCatch(error.status);
          });
    }
  }
}
