import { Component, OnInit, ViewChild } from '@angular/core';
import { Item } from '../../interface/item.interface';
import { ItemService } from '../../services/item.service';
import { Category } from '../../interface/category.interface';
import { CategoryService } from '../../services/category.service';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog, MatSnackBar } from '@angular/material';
import { ItemModalComponent } from './modal/item.modal.component';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})

export class ItemsComponent implements OnInit {
  items: Item[];
  categories: Category[] = [];
  category: Category;

  displayedColumns = ['orderNumber', 'value', 'description', 'barCode', 'action'];
  dataSource = new MatTableDataSource<Item>(this.items);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private itemService: ItemService, private categoryService: CategoryService, public dialog: MatDialog, public snackBar: MatSnackBar) { }

  ngOnInit() {
    this.categoryService.getCategories().subscribe((res: any) => {
      this.categories = res.data;
    })
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  openItemModal(item: Item) {
    let dialogRef = this.dialog.open(ItemModalComponent, {
      width: '30%',
      data: { 'categories': this.categories, 'item': item }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != undefined) {
        if (item != null && item.category.categoryId != result.category.categoryId) {
          this.items.splice(this.items.indexOf(item), 1);
          this.dataSource.data = this.items;
        } else {
          this.items.unshift(result);
          this.dataSource.data = this.items;
        }
      }

    });
  }

  onCategoryChange(categoryId: string) {
    this.itemService.getItems(categoryId).subscribe((res: any) => {
      this.items = res.data;
      this.dataSource.data = this.items;
    });
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  openCategoryModal(category: Category) {
    console.log(category);
  }

  onDeleteItem(item: Item) {
    console.log(item);
    this.itemService.deleteItem(item.inventoryItemId).subscribe((res: any) => {
      if (res != undefined) {
        this.items.splice(this.items.indexOf(item), 1);
        this.dataSource.data = this.items;
        this.openSnackBar(res.message, 'Close');
      }
    });
  }


  public openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 4000,
    });
  }
}
