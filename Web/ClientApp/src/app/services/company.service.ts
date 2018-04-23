import { Injectable } from '@angular/core';
import { ServiceHelper } from './service.helper';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';
import { Company } from '../interface/company.interface';

@Injectable()
export class CompanyService extends ServiceHelper {

  constructor(private http: HttpClient, public snackBar: MatSnackBar) {
    super(snackBar);
  }

  getCompanies() {
    return this.http.get(this.apiAddress + '/company/list', this.generateHeaders());
  }

  addCompany(company: Company) {
    return this.http.post(this.apiAddress + '/company/add', company, this.generateHeaders())
  }

  updateCompany(company: Company) {
    return this.http.put(this.apiAddress + '/company/update', company, this.generateHeaders())
  }

  deleteCompany(companyId: string) {
    return this.http.delete(this.apiAddress + '/company/delete/' + companyId, this.generateHeaders())
  }

}
