import { Component, OnInit, Inject } from '@angular/core';
import { CategoryService } from '../../../services/category.service';
import { MatSnackBar, MatDialogRef, MAT_DIALOG_DATA, MatChipInputEvent } from '@angular/material';
import { ServiceHelper } from '../../../services/service.helper';
import { Category } from '../../../interface/category.interface';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-category-modal',
  templateUrl: './category.modal.component.html',
  styleUrls: ['./category.modal.component.css']
})
export class CategoryModalComponent extends ServiceHelper implements OnInit {
  categories: Category[];
  category: Category = <Category>{};

  panelOpenState: boolean = false;
  showPanel: boolean = false;

  nameFormControl = new FormControl('', [
    Validators.required
  ]);

  constructor(public dialogRef: MatDialogRef<CategoryModalComponent>, @Inject(MAT_DIALOG_DATA) public data: any, public snackBar: MatSnackBar, private categoryService: CategoryService) {

    super(snackBar);

  }

  ngOnInit() {
    this.categories = this.data.categories;
  }

  showNewCategoryForm() {
    this.showPanel = !this.showPanel;
  }

  onSaveCategory(category: Category) {
    if (category.categoryId != null) {
      this.categoryService.updateCategory(category).subscribe((res: any) => {
        this.openSnackBar(res.message, 'Close');
        //this.dialogRef.close(this.categories);
      });
    } else {
      this.categoryService.addCategory(this.category.name).subscribe((res: any) => {
        this.openSnackBar(res.message, 'Close');
        this.categories.unshift(res.data);
        this.showNewCategoryForm();
        this.category = <Category>{};
        //this.dialogRef.close(this.categories);
      });
    }

  }

  onDeleteCategory(category: Category) {
    this.categoryService.deleteCategory(category.categoryId).subscribe((res: any) => {
      this.categories.splice(this.categories.indexOf(category), 1);
      this.openSnackBar(res.message, 'Close');
    });
  }

  onNoClick(): void {
    this.dialogRef.close('undo');
  }

}
