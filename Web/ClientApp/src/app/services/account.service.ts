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

  // Observable navItem source
  private authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this.authNavStatusSource.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router,
    public snackBar: MatSnackBar) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    this.authNavStatusSource.next(this.loggedIn);
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
      this.getUserDetails();
      this.loggedIn = true;
      this.authNavStatusSource.next(true);
      
      this.openSnackBar('User logged in successfully!','Close');
      return this.router.navigate(['/home']);
    });
  }

  isLoggedIn() {
    return !!localStorage.getItem('auth_token');
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this.authNavStatusSource.next(false);
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
    return this.http.get(this.apiAddress + "/user/details", this.generateHeadersWithToken()).subscribe((res: any) => this.userDetailsHolder.next(res.data));
  }

  register(user: any) {
    return this.http.post(this.apiAddress + '/register', user, this.generateHeaders())
      .subscribe((res: any) => {
        console.log(res);
        return this.router.navigate(['/login']);
      });
  }

  private generateHeadersWithToken() {
    let authToken = localStorage.getItem('auth_token');

    return {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': `${authToken}` })
    }
  }

  private generateHeaders() {
    return {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
  }
}
