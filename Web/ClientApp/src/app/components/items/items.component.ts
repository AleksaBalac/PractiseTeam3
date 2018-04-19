import { Component, OnInit, ViewChild } from '@angular/core';
import { Item } from '../../interface/item.interface';
import { ItemService } from '../../services/item.service';
import { Category } from '../../interface/category.interface';
import { CategoryService } from '../../services/category.service';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';

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

  constructor(private itemService: ItemService, private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService.getCategories().subscribe((res: any) => {
      this.categories = res.data;
    })
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  onCategoryChange(categoryId: string) {
    this.itemService.getItems(categoryId).subscribe((res: any) => {
      console.log(res.data);
      this.items = res.data;
      this.dataSource.data = this.items;
    });
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  openCategoryModal(category:Category) {
    console.log(category);
  }

  onDeleteItem(item:Item) {
    console.log(item);
  }

}
