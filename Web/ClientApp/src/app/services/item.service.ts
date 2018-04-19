import { Injectable } from '@angular/core';
import { ServiceHelper } from './service.helper';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ItemService extends ServiceHelper {

  constructor(
    private http: HttpClient, private router: Router, public snackBar: MatSnackBar) {
    super(snackBar);
  }

  getItems() {
    //return this.http.get(this.apiAddress + '/items/list', this.generateHeadersWithToken());
  }

  addItem(item:any) {
    //return this.http.get(this.apiAddress + '/users/list', this.generateHeadersWithToken());
  }

}
