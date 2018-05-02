import { Component, OnInit, ViewChild } from '@angular/core';
import { Item } from '../../interface/item.interface';
import { ItemService } from '../../services/item.service';
import { Category } from '../../interface/category.interface';
import { CategoryService } from '../../services/category.service';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog, MatSnackBar } from '@angular/material';
import { ItemModalComponent } from './modal/item.modal.component';
import { CategoryModalComponent } from './modal/category.modal.component';
import { SelectionModel } from '@angular/cdk/collections';
import { ExcelService } from '../../services/excel.service';


@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})

export class ItemsComponent implements OnInit {
  items: Item[];
  categories: Category[];
  category: string;
  showSpinner: boolean = true;

  displayedColumns = ['select', 'name', 'orderNumber', 'value', 'description', 'action'];
  dataSource = new MatTableDataSource<Item>(this.items);
  selection = new SelectionModel<Item>(true, []);

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild((MatSort) as any) sort: MatSort;

  constructor(private itemService: ItemService, private categoryService: CategoryService, public dialog: MatDialog, public snackBar: MatSnackBar, private excelService: ExcelService) { }

  ngOnInit() {
    this.getCategories();
  }

  getCategories() {
    this.categoryService.getCategories().subscribe((res: any) => {
      this.categories = res.data;
      this.showSpinner = false;
    });
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
      if (result !== 'undo' && result != undefined) {

        this.updateItemRow(item == null ? 'add' : 'edit', result.data);

      } else {
        this.onCategoryChange(String(this.category));
      }

    });
  }

  updateItemRow(mode: string, item: Item) {
    if (mode === 'add') {
      if (item.category.categoryId === this.category) {
        this.items.unshift(item);
      } else {
        this.openSnackBar(`Item is added in ${item.category.name}`, 'Close');
      }
    } else {
      if (item.category.categoryId !== this.category) {
        let originalItem = this.items.find(it => it.inventoryItemId === item.inventoryItemId);
        let index = this.items.indexOf(originalItem);

        this.items.splice(index, 1);
        this.openSnackBar(`Item is moved to ${item.category.name}`, 'Close');
      }
    }

    this.dataSource.data = this.items;
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

  openCategoryModal() {
    let dialogRef = this.dialog.open(CategoryModalComponent, {
      width: '50%',
      data: { 'categories': this.categories }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getCategories();
    });
  }

  onDeleteItem(item: Item) {
    this.itemService.deleteItem(item.inventoryItemId).subscribe((res: any) => {
      if (res != undefined) {
        this.items.splice(this.items.indexOf(item), 1);
        this.dataSource.data = this.items;
        this.openSnackBar(res.message, 'Close');
      }
    });
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

  onExportToExcel() {
    let list: any = [];

    this.selection.selected.forEach(item => {
      let inventoryItem: any = {
        name: item.name,
        description: item.description,
        orderNumber: item.orderNumber,
        value: item.value,
        category: item.category.name
      }

      list.push(inventoryItem);
    });

    this.excelService.exportAsExcelFile(list.sort(), 'InventoryItems');
  }

  public openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 4000,
    });
  }
}
