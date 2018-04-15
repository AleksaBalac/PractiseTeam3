import { NgModule } from '@angular/core';

import {
  MatButtonModule,
  MatMenuModule,
  MatToolbarModule,
  MatIconModule,
  MatCardModule,
  MatCheckboxModule,
  MatButtonToggleModule,
  MatSidenavModule,
  MatListModule,
  MatInputModule
  } from '@angular/material';


@NgModule({
  imports: [
    MatButtonModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatSidenavModule,
    MatCheckboxModule,
    MatButtonToggleModule,
    MatListModule,
    MatInputModule
  ],
  exports: [
    MatButtonModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatSidenavModule,
    MatCheckboxModule,
    MatButtonToggleModule,
    MatListModule,
    MatInputModule
  ]
})
export class MaterialModule { }
