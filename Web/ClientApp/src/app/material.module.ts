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
  MatInputModule,
  MatSnackBarModule,
  MatTableModule,
  MatPaginatorModule,
  MatDialogModule,
  MatOptionModule,
  MatSelectModule,
  MatProgressSpinnerModule,
  MatSortModule,
  MatChipsModule
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
    MatInputModule,
    MatSnackBarModule,
    MatTableModule,
    MatPaginatorModule,
    MatDialogModule,
    MatOptionModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    MatSortModule,
    MatChipsModule
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
    MatInputModule,
    MatSnackBarModule,
    MatTableModule,
    MatPaginatorModule,
    MatDialogModule,
    MatOptionModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    MatSortModule,
    MatChipsModule
  ]
})
export class MaterialModule { }
