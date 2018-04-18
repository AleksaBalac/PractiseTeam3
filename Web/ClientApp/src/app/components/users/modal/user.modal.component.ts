import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { FormControl, Validators } from '@angular/forms';
import { User } from '../../../interface/user.interface';
import { UsersService } from '../../../services/users.service';
import { ServiceHelper } from '../../../services/service.helper';

@Component({
  selector: 'app-modal',
  templateUrl: './user.modal.component.html',
  styleUrls: ['./user.modal.component.css']
})
export class UserModalComponent extends ServiceHelper implements OnInit {
  user: any = {};
  mode:string;

  firstNameFormControl = new FormControl('', [
    Validators.required
  ]);

  lastNameFormControl = new FormControl('', [
    Validators.required
  ]);

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);

  constructor(
    private userService: UsersService,
    public dialogRef: MatDialogRef<UserModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public snackBar: MatSnackBar) {
    super(snackBar);
  }

  ngOnInit() {
    if (this.data.user != null) {
      this.user = this.data.user;
      this.mode = 'edit';
    } else this.mode = 'add';
  }

  onSave() {
    if (this.mode === 'add') {
      this.userService.saveUser(this.user).subscribe((res: any) => {
        this.openSnackBar(res.message, 'Close');
          this.dialogRef.close(this.user);
        },
        error => {
          console.log(error);
        });
    } else {
      this.userService.updateUser(this.user).subscribe((res: any) => {
          this.openSnackBar(res.message, 'Close');
          this.dialogRef.close(this.user);
        },
        error => {
          console.log(error);
        });
    }

  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
