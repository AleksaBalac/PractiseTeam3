import { Component, OnInit, Inject } from '@angular/core';
import { CompanyService } from '../../../services/company.service';
import { Company } from '../../../interface/company.interface';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { ServiceHelper } from '../../../services/service.helper';

@Component({
  selector: 'app-company.modal',
  templateUrl: './company.modal.component.html',
  styleUrls: ['./company.modal.component.css']
})
export class CompanyModalComponent extends ServiceHelper implements OnInit {
  private company: Company = <Company>{};

  private mode: string;

  constructor(public dialogRef: MatDialogRef<CompanyModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public snackBar: MatSnackBar,
    private companyService: CompanyService) {

    super(snackBar);

  }

  ngOnInit() {
    if (this.data.company != null) {
      this.company = this.data.company;
      this.mode = 'edit';
    } else this.mode = 'add';
  }

  onSave() {
    //if (this.mode === 'add') {
    //  this.itemService.addItem(this.item).subscribe((res: any) => {
    //    this.openSnackBar(res.message, 'Close');
    //    this.dialogRef.close(res.data);
    //  },
    //    error => {
    //      console.log(error);
    //    });
    //} else {
    //  this.itemService.updateItem(this.item).subscribe((res: any) => {
    //    this.openSnackBar(res.message, 'Close');
    //    this.dialogRef.close(res.data);
    //  },
    //    error => {
    //      console.log(error);
    //    });
    //}
  }

  onNoClick(): void {
    this.dialogRef.close('undo');
  }

}
