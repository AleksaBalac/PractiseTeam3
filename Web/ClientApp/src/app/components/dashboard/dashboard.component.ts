import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'dashboard-home',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.userDetails.subscribe((res: any) => console.log(res));
  }
}
