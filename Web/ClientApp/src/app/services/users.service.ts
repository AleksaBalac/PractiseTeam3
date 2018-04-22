import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/Rx';
import { Router } from '@angular/router';
import { ServiceHelper } from './service.helper';

@Injectable()
export class UsersService extends ServiceHelper {

  constructor(
    private http: HttpClient, private router: Router, public snackBar: MatSnackBar) {
    super(snackBar);
  }

  getUsers() {
    return this.http.get(this.apiAddress + '/users/list', this.generateHeadersWithToken());
  }

  getRoles() {
    return this.http.get(this.apiAddress + '/users/list/roles', this.generateHeadersWithToken());
  }

  saveUser(user: any) {
    return this.http.post(this.apiAddress + '/users/add', user, this.generateHeadersWithToken());
  }

  updateUser(user: any) {
    return this.http.put(this.apiAddress + '/users/update', user, this.generateHeaders());
  }

  deleteUser(userId: string) {
    return this.http.delete(this.apiAddress + '/users/delete/' + userId,  this.generateHeaders());
  }

}
