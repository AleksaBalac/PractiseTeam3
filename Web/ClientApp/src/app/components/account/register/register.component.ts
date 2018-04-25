import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from './../../../services/account.service';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
  user: any = {};

  firstNameFormControl = new FormControl('', [
    Validators.required
  ]);

  lastNameFormControl = new FormControl('', [
    Validators.required
  ]);

  nameFormControl = new FormControl('', [
    Validators.required
  ]);

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required
  ]);

  constructor(
    private accountService: AccountService,
    private router: Router,
    public snackBar: MatSnackBar) { }

  ngOnInit() {
  }

  onRegister() {
    this.accountService.register(this.user).subscribe((res: any) => {
        console.log(res);
        //return this.router.navigate(['/login']);
      this.openSnackBar(res.message, 'Close');
      this.user = {};
    }, error => {
        this.openSnackBar(error.error.message, 'Close');
      });
  }

  public openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 4000,
    });
  }

}
