import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { ServiceHelper } from './service.helper';
import 'rxjs/add/operator/map';

@Injectable()
export class AccountService extends ServiceHelper {

  constructor(private http: HttpClient) {
    super();
  }

  login(user:any) {
    return this.http.post(this.apiAddress + '/login', this.generateHeaders());
  }

  register() {

  }


  private generateHeaders() {
    return {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
  }
}
