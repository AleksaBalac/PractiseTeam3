import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource, MatPaginator, MatDialog, MatSnackBar } from '@angular/material';
import { CompanyService } from '../../services/company.service';
import { Company } from '../../interface/company.interface';
import { CompanyModalComponent } from './modal/company.modal.component';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  styleUrls: ['./companies.component.css']
})

export class CompaniesComponent implements OnInit {
  companies: Company[];

  displayedColumns = ['name', 'address', 'validLicenceTill', 'contactPerson', 'action'];
  dataSource = new MatTableDataSource<Company>(this.companies);

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private companyService: CompanyService, public dialog: MatDialog, public snackBar: MatSnackBar) { }

  ngOnInit() {
    this.getCompanies();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  openCompanyModal(company: Company) {
    let dialogRef = this.dialog.open(CompanyModalComponent, {
      width: '40%',
      data: { 'company': company }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != undefined && result !== 'undo') {
        this.companies.push(result.data);
        this.dataSource.data = this.companies;
      } else {
        this.getCompanies();
      }

    });
  }

  getCompanies() {
    this.companyService.getCompanies().subscribe((res: any) => {
      this.companies = res.data;
      this.dataSource.data = this.companies;
    });
  }

  onDeleteCompany(company: Company) {
    this.companyService.deleteCompany(company.companyId).subscribe((res: any) => {
      let index = this.companies.indexOf(company);
      this.companies.splice(index, 1);
      this.dataSource.data = this.companies;
      this.openSnackBar(res.message, "Close");
    });
  }

  public openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 4000,
    });
  }
}
