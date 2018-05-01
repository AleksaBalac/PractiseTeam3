import { Injectable } from '@angular/core';
import { ServiceHelper } from './service.helper';
import { MatSnackBar } from '@angular/material';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DashboardService extends ServiceHelper{

  constructor(
    private http: HttpClient, public snackBar: MatSnackBar) {
    super(snackBar);
  }

  getCompanyDetails() {
    return this.http.get(this.apiAddress + '/dashboard/company', this.generateHeadersWithToken());
  }

  getCategoryDetails() {
    return this.http.get(this.apiAddress + '/dashboard/category', this.generateHeadersWithToken());
  }

}
