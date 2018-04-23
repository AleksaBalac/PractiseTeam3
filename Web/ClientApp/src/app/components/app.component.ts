import { Component, OnInit } from '@angular/core';
import { AccountService } from './../services/account.service';
import { User } from '../interface/user.interface';
import { LoggedInUser } from '../interface/logged.user.interface';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  events = [];
  userDetails: LoggedInUser = <LoggedInUser>{};

  constructor(private user: AccountService) { }

  ngOnInit() {
    this.getUserDetails();
  }

  getUserDetails() {
    this.user.userDetails.subscribe((user: LoggedInUser) => {
      this.userDetails = user;
    });
  }

  onLogOut() {
    this.user.logout();
  }
}
