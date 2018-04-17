import { Component, OnInit } from '@angular/core';
import { AccountService } from './../../services/account.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  userDetails: any;


  constructor(private user: AccountService) {
    
  }

  ngOnInit(): void {
    //this.user.getUserDetails().subscribe((res: any) => this.userDetails = res.data);
    this.user.userDetails.subscribe((res: any) => this.userDetails = res);
    
  }

  onLogOut() {
    this.user.logout();
  }

}
