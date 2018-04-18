import { environment } from '../../environments/environment';
import { HttpHeaders } from '@angular/common/http';


export class ServiceHelper {
  public apiAddress: string = environment.apiAddress;

  constructor() { }

  public generateHeadersWithToken() {
    let authToken = localStorage.getItem('auth_token');

    return {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': `${authToken}` })
    }
  }

  public generateHeaders() {
    return {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
  }
}
