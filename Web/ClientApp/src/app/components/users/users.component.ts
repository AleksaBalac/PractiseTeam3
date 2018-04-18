import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UsersService } from '../../services/users.service';
import { UserModalComponent } from './modal/user.modal.component';
import { User } from '../../interface/user.interface';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})

export class UsersComponent implements OnInit, AfterViewInit {
  user: User[] = [];
  roles: any;

  displayedColumns = ['firstName', 'lastName', 'email', 'role', 'action'];
  dataSource = new MatTableDataSource<User>(this.user);
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private userService: UsersService, public dialog: MatDialog) { }

  ngOnInit() {
    this.userService.getUsers().subscribe((res: any) => {
      this.user = res.data;
      this.dataSource.data = this.user;
    })

    this.userService.getRoles().subscribe((res: any) => {
      this.roles = res.data
    });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  openAddUserModal() {
    let dialogRef = this.dialog.open(UserModalComponent, {
      width: '60%',
      data: this.roles
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != undefined) {
        console.log('The dialog was closed', result);
        this.user.unshift(result);
        this.dataSource.data = this.user;
      }
    });
  }
}


