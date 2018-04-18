import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormControl, Validators } from '@angular/forms';
import { User } from '../../../interface/user.interface';
import { UsersService } from '../../../services/users.service';

@Component({
  selector: 'app-modal',
  templateUrl: './user.modal.component.html',
  styleUrls: ['./user.modal.component.css']
})
export class UserModalComponent implements OnInit {
  user: any = {};

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
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }

  onSave() {
    this.userService.saveUser(this.user).subscribe((res: any) => {
      this.dialogRef.close(this.user);
    }, error => {
      console.log(error);
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
