import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/Rx';
import { Router } from '@angular/router';
import { ServiceHelper } from './service.helper';
import 'rxjs/add/operator/map';
import { MatSnackBar } from '@angular/material';

@Injectable()
export class AccountService extends ServiceHelper {
  private loggedIn = false;

  private userDetailsHolder = new BehaviorSubject<any>({});//TODO change this to model or interface
  userDetails = this.userDetailsHolder.asObservable();
  
  constructor(
    private http: HttpClient,
    private router: Router,
    public snackBar: MatSnackBar) {

    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    this.getUserDetails();
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 4000,
    });
  }

  login(user: any) {
    return this.http.post(this.apiAddress + '/login', user, this.generateHeaders()).subscribe((res: any) => {
      localStorage.setItem('auth_token', res.data.auth_token);
      console.log('log in before getUserDetails');
      this.getUserDetails();
      console.log('log in after getUserDetails');
      this.loggedIn = true;
      
      this.openSnackBar('User logged in successfully!','Close');
      return this.router.navigate(['/dashboard']);
    });
  }

  isLoggedIn() {
    return !!localStorage.getItem('auth_token');
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this.userDetailsHolder.next(null);
    this.openSnackBar('User logged out successfully!', 'Close');
    this.router.navigate(['/login']);
  }

  getUserDetails() {
    if (!this.isLoggedIn()) {
      return null;
    }
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    console.log('im going to take user details data on server');
    return this.http.get(this.apiAddress + "/user/details", this.generateHeadersWithToken()).subscribe((res: any) => this.userDetailsHolder.next(res.data));
  }

  register(user: any) {
    return this.http.post(this.apiAddress + '/register', user, this.generateHeaders())
      .subscribe((res: any) => {
        console.log(res);
        return this.router.navigate(['/login']);
      });
  }

  
}
