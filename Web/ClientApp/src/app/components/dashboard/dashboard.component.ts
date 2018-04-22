import { Component, OnInit } from '@angular/core';
import { LoggedInUser } from '../../interface/logged.user.interface';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'dashboard-home',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(private user: AccountService) { }

  ngOnInit() {
    this.user.getUserDetails();
  }
}
