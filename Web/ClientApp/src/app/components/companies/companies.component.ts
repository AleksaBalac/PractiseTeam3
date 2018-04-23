import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatTableDataSource, MatPaginator, MatDialog } from '@angular/material';
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

  constructor(private companyService: CompanyService, public dialog: MatDialog, ) { }

  ngOnInit() {
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

      } else {

      }

    });
  }

}
