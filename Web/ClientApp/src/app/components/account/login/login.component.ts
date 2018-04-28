import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
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
  isSuccess: boolean = false;
  showSpinner: boolean = false;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private snackBar: MatSnackBar) { }


  ngOnInit(): void {
    this.createForm();
    this.getRouteParam();
  }

  getRouteParam() {
    const param = this.activatedRoute.snapshot.queryParams["isSuccess"];
    if (param) this.isSuccess = true;
  }

  createForm() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onLogin() {
    this.showSpinner = true;
    if (!this.loginForm.valid) {
      this.showSpinner = false;
      return this.openSnackBar('You must provide required data', 'Close');
    }

    this.accountService.login(this.user).subscribe((res: any) => {

      localStorage.setItem('auth_token', res.data.auth_token);
      //this.user.getUserDetails();
      //this.user.loggedIn = true;

      this.openSnackBar(res.message, 'Close');
      return this.router.navigate(['/dashboard']);

    }, error => {
      this.showSpinner = false;
      console.log(error);
      if (error.error == undefined || error.error.message == undefined) {
        this.openSnackBar(error.message, 'Close');
      } else {
        this.openSnackBar(error.error.message, 'Close');
      }
    });
  }

  public openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 4000,
    });
  }
}
