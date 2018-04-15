import { environment } from '../../environments/environment';

export class ServiceHelper {
  public apiAddress: string = environment.apiAddress;

  constructor() {  }
}
