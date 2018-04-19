import { Component, OnInit, Inject } from '@angular/core';
import { Item } from '../../../interface/item.interface';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { ServiceHelper } from '../../../services/service.helper';
import { ItemService } from '../../../services/item.service';
import { FormControl, Validators } from '@angular/forms';
import { Category } from '../../../interface/category.interface';

@Component({
  selector: 'app-item-modal',
  templateUrl: './item.modal.component.html',
  styleUrls: ['./item.modal.component.css']
})

export class ItemModalComponent extends ServiceHelper implements OnInit {
  item: Item = <Item>{
    barCode: '',
    description: '',
    inventoryItemId: '',
    orderNumber: 0,
    value: '',
    category: <Category>{
      name: ''
    }
  };

  mode: string;

  valueFormControl = new FormControl('', [
    Validators.required
  ]);

  descriptionFormControl = new FormControl('', [
    Validators.required, Validators.maxLength(300)
  ]);

  constructor(public dialogRef: MatDialogRef<ItemModalComponent>, @Inject(MAT_DIALOG_DATA) public data: any, public snackBar: MatSnackBar, private itemService: ItemService) {

    super(snackBar);

  }

  ngOnInit() {
    if (this.data.item != null) {
      this.item = this.data.item;
      this.mode = 'edit';
    } else this.mode = 'add';
  }

  onSave() {
    if (this.mode === 'add') {
      this.itemService.addItem(this.item).subscribe((res: any) => {
        this.openSnackBar(res.message, 'Close');
        this.dialogRef.close(res.data);
      },
        error => {
          console.log(error);
        });
    } else {
      this.itemService.updateItem(this.item).subscribe((res: any) => {
        this.openSnackBar(res.message, 'Close');
        this.dialogRef.close(res.data);
      },
        error => {
          console.log(error);
        });
    }

  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
