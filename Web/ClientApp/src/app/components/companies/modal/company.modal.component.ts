import { Component, OnInit, Inject } from '@angular/core';
import { CompanyService } from '../../../services/company.service';
import { Company } from '../../../interface/company.interface';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { ServiceHelper } from '../../../services/service.helper';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { User } from '../../../interface/user.interface';

@Component({
  selector: 'app-company-modal', //app-company.modal zbog ovog sranja nije htelo da radi
  templateUrl: './company.modal.component.html',
  styleUrls: ['./company.modal.component.css']
})

export class CompanyModalComponent extends ServiceHelper implements OnInit {
  private company: Company = <Company>{ companyAdmin: <User>{} };
  private mode: string;
  private hideAdministrator: boolean = true;

  companyForm: FormGroup;

  constructor(public dialogRef: MatDialogRef<CompanyModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public snackBar: MatSnackBar,
    private companyService: CompanyService,
    private fb: FormBuilder, ) {

    super(snackBar);
  }

  ngOnInit() {
    if (this.data.company != null) {
      this.company = this.data.company;
      this.mode = 'edit';
      this.hideAdministrator = false;
    } else {
      this.mode = 'add';
    }

    this.createForm();
  }

  createForm() {
    if (this.mode === 'add') {
      this.companyForm = this.fb.group({
        name: ['', [Validators.required, Validators.maxLength(20)]],
        address: ['', [Validators.required, Validators.maxLength(40)]],
        contactPerson: ['', [Validators.required, Validators.maxLength(40)]],
        validLicenceTill: ['', [Validators.required]],
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        email: ['', [Validators.required, Validators.email]]
      });

    } else {
      this.companyForm = this.fb.group({
        name: ['', [Validators.required, Validators.maxLength(20)]],
        address: ['', [Validators.required, Validators.maxLength(40)]],
        contactPerson: ['', [Validators.required, Validators.maxLength(40)]],
        validLicenceTill: ['', [Validators.required]]
      });
    }
  }

  onSave() {
    if (!this.companyForm.valid) {
      return this.openSnackBar('You must provide required data', 'Close');
    }

    if (this.mode === 'add') {
      this.companyService.addCompany(this.company).subscribe((res: any) => {
        this.openSnackBar(res.message, 'Close');
        this.dialogRef.close(res.data);
      },
        error => {
          this.openSnackBar(error.error.message, 'Close');
        });
    } else {
      this.companyService.updateCompany(this.company).subscribe((res: any) => {
        this.openSnackBar(res.message, 'Close');
        this.dialogRef.close(res.data);
      },
        error => {
          this.openSnackBar(error.error.message, 'Close');
        });
    }
  }

  onNoClick(): void {
    this.dialogRef.close('undo');
  }

}
