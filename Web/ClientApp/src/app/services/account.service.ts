import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Subject } from 'rxjs/Rx';
import { Router } from '@angular/router';
import { ServiceHelper } from './service.helper';
import 'rxjs/add/operator/map';
import { MatSnackBar } from '@angular/material';
import { LoggedInUser } from '../interface/logged.user.interface';

@Injectable()
export class AccountService extends ServiceHelper {
  private loggedIn = false;

  // userDetails:Subject<LoggedInUser> = new BehaviorSubject<LoggedInUser>(<LoggedInUser>{});
  userDetails = new Subject();
  //userDetails = this.userDetailsHolder.asObservable();

  constructor(private http: HttpClient, private router: Router, public snackBar: MatSnackBar) {

    super(snackBar);


    this.loggedIn = !!localStorage.getItem('auth_token');
    this.getUserDetails();

  }

  login(user: any) {
    return this.http.post(this.apiAddress + '/login', user, this.generateHeaders()).subscribe((res: any) => {

      localStorage.setItem('auth_token', res.data.auth_token);
      this.getUserDetails();
      this.loggedIn = true;

      this.openSnackBar(res.message, 'Close');
      return this.router.navigate(['/dashboard']);

    }, error => {
      if (error.error == undefined || error.error.message == undefined) {
        this.openSnackBar(error, 'Close');
      } else {
        this.openSnackBar(error.error.message, 'Close');

      }
    });
  }

  isLoggedIn() {
    return !!localStorage.getItem('auth_token');
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this.userDetails.next(<LoggedInUser>{});
    this.openSnackBar('User logged out successfully!', 'Close');
    this.router.navigate(['/login']);
  }

  getUserDetails() {
    if (!this.isLoggedIn()) {
      return null;
    }
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.get(this.apiAddress + "/user/details", this.generateHeadersWithToken()).subscribe((res: any) => {
      let user = <LoggedInUser>{};
      user.userRole = res.data.userRole;
      user.fullName = res.data.fullName;
      user.companyName = res.data.companyName;
      this.userDetails.next(user);
    });
  }

  register(user: any) {
    return this.http.post(this.apiAddress + '/register', user, this.generateHeaders())
      .subscribe((res: any) => {
        console.log(res);
        return this.router.navigate(['/login']);
      }, error => {
        this.openSnackBar(error.error.message, 'Close');
      });
  }
}
