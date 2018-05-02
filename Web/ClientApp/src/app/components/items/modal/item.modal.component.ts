import { Component, OnInit, Inject } from '@angular/core';
import { Item } from '../../../interface/item.interface';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { ServiceHelper } from '../../../services/service.helper';
import { ItemService } from '../../../services/item.service';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Category } from '../../../interface/category.interface';

@Component({
  selector: 'app-item-modal',
  templateUrl: './item.modal.component.html',
  styleUrls: ['./item.modal.component.css']
})

export class ItemModalComponent extends ServiceHelper implements OnInit {
  item: Item = <Item>{
     category: <Category>{}
  };

  itemForm: FormGroup;

  mode: string;
  
  constructor(
    public dialogRef: MatDialogRef<ItemModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public snackBar: MatSnackBar,
    private itemService: ItemService,
    private fb: FormBuilder) {

    super(snackBar);

  }

  ngOnInit() {
    if (this.data.item != null) {
      this.item = this.data.item;
      this.mode = 'edit';
    } else {
      this.mode = 'add';
    }

    this.createForm();
  }

  createForm() {
    this.itemForm = this.fb.group({
      name: ['', [Validators.required]],
      value: ['', [Validators.required]],
      description: ['', [Validators.required]],
      category: ['', [Validators.required]]
    });
  }

  onSave() {
    if (!this.itemForm.valid) {
      return this.openSnackBar('You must provide required data', 'Close');
    }

    if (this.mode === 'add') {
      this.itemService.addItem(this.item).subscribe((res: any) => {
        this.openSnackBar(res.message, 'Close');
        this.dialogRef.close(res);
      },
        error => {
          console.log(error);
        });
    } else {
      this.itemService.updateItem(this.item).subscribe((res: any) => {
        this.openSnackBar(res.message, 'Close');
        this.dialogRef.close(res);
      },
        error => {
          console.log(error);
        });
    }

  }

  onNoClick(): void {
    this.dialogRef.close('undo');
  }

}
