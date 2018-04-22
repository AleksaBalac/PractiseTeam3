import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from './../../../services/account.service';
import { MatSnackBar } from '@angular/material';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [AccountService]
})

export class LoginComponent implements OnInit {
  user: any = {};
  loginForm: FormGroup;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private fb: FormBuilder,
    private snackBar: MatSnackBar) { }


  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4)]],
    });
  }


  onLogin() {
    if (!this.loginForm.valid) {
      return this.openSnackBar('You must provide required data', 'Close');
    }

    this.accountService.login(this.user);
  }

  public openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 4000,
    });
  }
}
