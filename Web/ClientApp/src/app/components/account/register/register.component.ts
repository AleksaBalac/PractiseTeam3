import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
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
  registerForm: FormGroup;


  constructor(
    private accountService: AccountService,
    private router: Router,
    private fb: FormBuilder,
    public snackBar: MatSnackBar) { }

  ngOnInit() {

    this.createForm();

  }

  createForm() {
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  onRegister() {
    if (!this.registerForm.valid) {
      return this.openSnackBar('You must provide required data', 'Close');
    }

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
