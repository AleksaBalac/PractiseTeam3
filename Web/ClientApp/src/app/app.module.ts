import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { XHRBackend } from '@angular/http';

import { RouterModule } from '@angular/router';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';

//components
import { AppComponent } from './components/app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CounterComponent } from './components/counter/counter.component';
import { ContactComponent } from './components/contact/contact.component';
import { CompaniesComponent } from './components/companies/companies.component';
import { UsersComponent } from './components/users/users.component';
import { ItemsComponent } from './components/items/items.component';
import { CompanyModalComponent } from './components/companies/modal/company.modal.component';

//modals
import { UserModalComponent } from './components/users/modal/user.modal.component';
import { ItemModalComponent } from './components/items/modal/item.modal.component';
import { CategoryModalComponent } from './components/items/modal/category.modal.component';

//acount
import { LoginComponent } from './components/account/login/login.component';
import { RegisterComponent } from './components/account/register/register.component';

//auth
import { AuthGuard } from './auth.guard';
import { AuthenticateXHRBackend } from './authenticate-xhr.backend';

//services
import { AccountService } from './services/account.service';
import { UsersService } from './services/users.service';
import { ItemService } from './services/item.service';
import { CategoryService } from './services/category.service';
import { ExcelService } from './services/excel.service';
import { CompanyService } from './services/company.service';
import { DashboardService } from './services/dashboard.service';

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
    ItemModalComponent,
    CategoryModalComponent,
    CompaniesComponent,
    CompanyModalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule,
    RouterModule.forRoot([
      //{ path: '**', component: DashboardComponent, canActivate: [AuthGuard] },
      { path: 'dashboard', component: DashboardComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
      { path: 'items', component: ItemsComponent, canActivate: [AuthGuard] },
      { path: 'companies', component: CompaniesComponent, canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'contact', component: ContactComponent }
    ])
  ],
  entryComponents: [
    UserModalComponent,
    ItemModalComponent,
    CategoryModalComponent,
    CompanyModalComponent
  ],
  providers:
    [
      AuthGuard, {
        provide: XHRBackend,
        useClass: AuthenticateXHRBackend
      },
      AccountService,
      UsersService,
      ItemService,
      CategoryService,
      ExcelService,
      CompanyService,
      DashboardService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
