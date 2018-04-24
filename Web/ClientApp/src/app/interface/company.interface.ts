import { User } from "./user.interface";

export interface Company {
  companyId: string;
  name: string;
  address: string;
  contactPerson: string;
  validLicenceTill: string;
  companyAdmin: User;
}
