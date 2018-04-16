import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';

import { ServiceHelper } from './service.helper';
import 'rxjs/add/operator/map';

@Injectable()
export class AccountService extends ServiceHelper {
  private loggedIn = false;

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  constructor(private http: HttpClient) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    this._authNavStatusSource.next(this.loggedIn);
  }

  login(user: any) {
    return this.http.post(this.apiAddress + '/login', user, this.generateHeaders()).subscribe((res: any) => {
      localStorage.setItem('auth_token', res.auth_token);
      this.loggedIn = true;
      this._authNavStatusSource.next(true);
      return true;
    });
  }

  isLoggedIn() {
    return !!localStorage.getItem('auth_token');
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  getHomeDetails() {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http.get(this.apiAddress + "/user", this.generateHeadersWithToken());
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
