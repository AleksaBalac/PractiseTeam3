import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator, MatTableDataSource, MatSnackBar } from '@angular/material';
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
  users: User[] = [];
  roles: any;
  showSpinner:boolean;

  displayedColumns = ['firstName', 'lastName', 'email', 'role', 'action'];
  dataSource = new MatTableDataSource<User>(this.users);
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private userService: UsersService, public dialog: MatDialog, public snackBar: MatSnackBar) { }

  ngOnInit() {
    this.showSpinner = true;

    this.userService.getUsers().subscribe((res: any) => {
      this.users = res.data;
      this.dataSource.data = this.users;
      this.showSpinner = false;
    });

    this.userService.getRoles().subscribe((res: any) => {
      this.roles = res.data;
    });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  openUserModal(user: User) {
    let dialogRef = this.dialog.open(UserModalComponent, {
      width: '30%',
      data: { 'role': this.roles, 'user': user }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != undefined && user == null) {
        this.users.unshift(result);
        this.dataSource.data = this.users;
      }
      else if (user != null && result != undefined) {
        user = result;
      }
    });
  }

  onDeleteUser(user: User) {
    this.userService.deleteUser(user.id).subscribe((res: any) => {
      if (res.success) {
        this.users.slice(1, this.users.indexOf(user));
        this.users.splice(this.users.indexOf(user), 1);
        this.dataSource.data = this.users;
        this.openSnackBar(res.message, 'Close');
      }
    });
  }

  public openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 4000,
    });
  }

}


