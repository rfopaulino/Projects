import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs'
import { ActivatedRoute, Router } from "@angular/router";
import 'rxjs/add/operator/map'

@Injectable()
export class AuthenticationService {

  private loggedIn = new BehaviorSubject<boolean>(false);

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router) { }

  login(dto) {
    return this.http.post<any>('http://localhost:55680/usuario/authentication', dto)
      .map(response => {
        if (response && response.accessToken) {
          this.loggedIn.next(true);
          localStorage.setItem('currentUser', JSON.stringify(response));
          this.router.navigate(['/home']);
        }
        return response;
      });
  }

  logout() {
    this.loggedIn.next(false);
    localStorage.removeItem('currentUser');
    this.router.navigate(['/']);
  }
}
