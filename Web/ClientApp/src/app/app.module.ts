import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Request, XHRBackend, BrowserXhr, ResponseOptions, XSRFStrategy, Response } from '@angular/http';

import { RouterModule } from '@angular/router';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';

import { AppComponent } from './components/app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CounterComponent } from './components/counter/counter.component';
import { ContactComponent } from './components/contact/contact.component';

import { UsersComponent } from './components/users/users.component';
import { UserModalComponent } from './components/users/modal/user.modal.component';

import { ItemsComponent } from './components/items/items.component';
import { ItemModalComponent } from './components/items/modal/item.modal.component';

import { LoginComponent } from './components/account/login/login.component';
import { RegisterComponent } from './components/account/register/register.component';

import { AuthGuard } from './auth.guard';
import { AuthenticateXHRBackend } from './authenticate-xhr.backend';

import { AccountService } from './services/account.service';
import { UsersService } from './services/users.service';
import { ItemService } from './services/item.service';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    DashboardComponent,
    CounterComponent,
    ContactComponent,
    LoginComponent,
    RegisterComponent,
    UsersComponent,
    UserModalComponent,
    ItemsComponent,
    ItemModalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule,
    RouterModule.forRoot([
      { path: 'dashboard', component: DashboardComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
      { path: 'items', component: ItemsComponent, canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'contact', component: ContactComponent }
    ])
  ],
  entryComponents: [
    UserModalComponent,
    ItemModalComponent
  ],
  providers:
    [
      AccountService,
      UsersService,
      ItemService,
      AuthGuard, {
        provide: XHRBackend,
        useClass: AuthenticateXHRBackend
      }
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
