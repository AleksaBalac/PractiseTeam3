import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AccountService } from './../../../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [AccountService]
})

export class LoginComponent {
  user: any = {};

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required
  ]);

  constructor(private accountService: AccountService) { }


  onLogin() {
    console.log(this.user);
    this.accountService.login(this.user).subscribe(res => console.log(res));
  }

}
