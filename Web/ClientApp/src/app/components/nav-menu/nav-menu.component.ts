import { Component, OnInit } from '@angular/core';
import { AccountService } from './../../services/account.service';
import { User } from '../../interface/user.interface';
import { LoggedInUser } from '../../interface/logged.user.interface';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  userDetails: LoggedInUser = <LoggedInUser>{};


  constructor(private user: AccountService) {

  }

  ngOnInit(): void {
    //this.user.getUserDetails().subscribe((res: any) => this.userDetails = res.data);
    this.user.userDetails.subscribe((res: LoggedInUser) => this.userDetails = res);
    
  }
}
