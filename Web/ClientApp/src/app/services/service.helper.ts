import { environment } from '../../environments/environment';
import { HttpHeaders } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';

export class ServiceHelper {
  public apiAddress: string = environment.apiAddress;
  
  constructor(public snackBar: MatSnackBar) { }

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

  public openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 4000,
    });
  }
}
