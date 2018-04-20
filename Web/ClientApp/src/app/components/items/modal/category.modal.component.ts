import { Component, OnInit, Inject } from '@angular/core';
import { CategoryService } from '../../../services/category.service';
import { MatSnackBar, MatDialogRef, MAT_DIALOG_DATA, MatChipInputEvent } from '@angular/material';
import { ServiceHelper } from '../../../services/service.helper';
import { Category } from '../../../interface/category.interface';
import {ENTER, COMMA} from '@angular/cdk/keycodes';

@Component({
  selector: 'app-category-modal',
  templateUrl: './category.modal.component.html',
  styleUrls: ['./category.modal.component.css']
})
export class CategoryModalComponent extends ServiceHelper implements OnInit {
  categories:Category[];

  visible: boolean = true;
  selectable: boolean = true;
  removable: boolean = true;
  addOnBlur: boolean = true;

  // Enter, comma
  separatorKeysCodes = [ENTER, COMMA];


  constructor(public dialogRef: MatDialogRef<CategoryModalComponent>, @Inject(MAT_DIALOG_DATA) public data: any, public snackBar: MatSnackBar, private categoryService: CategoryService) {

    super(snackBar);

  }

  ngOnInit() {
    this.categories = this.data.categories;
  }

  add(event: MatChipInputEvent): void {
    let input = event.input;
    let value = event.value;

    // Add our fruit
    if ((value || '').trim()) {
      //this.categories.push({ name: value.trim() });
      console.log('dodaj', value);
      //this.categories.push((value) as any);
      this.categoryService.addCategory(value).subscribe((res: any) => {
        this.categories.push(res.data);
        this.openSnackBar(res.message, 'Close');
      });
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(category: Category): void {
    let index = this.categories.indexOf(category);

    if (index >= 0) {
      this.categories.splice(index, 1);
    }
  }

  onSave() {
    //if (this.mode === 'add') {
    //  this.itemService.addItem(this.item).subscribe((res: any) => {
    //    this.openSnackBar(res.message, 'Close');
    //    this.dialogRef.close(res.data);
    //  },
    //    error => {
    //      console.log(error);
    //    });
    //} else {
    //  this.itemService.updateItem(this.item).subscribe((res: any) => {
    //    this.openSnackBar(res.message, 'Close');
    //    this.dialogRef.close(res.data);
    //  },
    //    error => {
    //      console.log(error);
    //    });
    //}

  }

  onNoClick(): void {
    this.dialogRef.close('undo');
  }

}
