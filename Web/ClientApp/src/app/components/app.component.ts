import { Component, OnInit } from '@angular/core';
import { AccountService } from './../services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  events = [];
  userDetails:any;

  constructor(private user: AccountService) { }

  ngOnInit() {
    this.user.userDetails.subscribe((res: any) => this.userDetails = res);
  }

  onLogOut() {
    this.user.logout();
  }
}
