import { Injectable } from '@angular/core';
import { ServiceHelper } from './service.helper';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class CategoryService extends ServiceHelper {

  constructor(
    private http: HttpClient, private router: Router, public snackBar: MatSnackBar) {
    super(snackBar);
  }

  getCategories() {
    return this.http.get(this.apiAddress + '/category/list', this.generateHeadersWithToken());
  }

  addCategory(item:any) {
    //return this.http.get(this.apiAddress + '/users/list', this.generateHeadersWithToken());
  }


}
