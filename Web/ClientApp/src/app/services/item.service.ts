import { Injectable } from '@angular/core';
import { ServiceHelper } from './service.helper';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { Item } from '../interface/item.interface';

@Injectable()
export class ItemService extends ServiceHelper {

  constructor(
    private http: HttpClient, private router: Router, public snackBar: MatSnackBar) {
    super(snackBar);
  }

  getItems(categoryId: string) {
    return this.http.get(this.apiAddress + '/items/list/' + categoryId, this.generateHeadersWithToken());
  }

  addItem(item: Item) {
    return this.http.post(this.apiAddress + '/items/add', item, this.generateHeadersWithToken());
  }

  updateItem(item: Item) {
    return this.http.put(this.apiAddress + '/items/update', item, this.generateHeaders());
  }

  deleteItem(itemId: string) {
    return this.http.delete(this.apiAddress + '/items/delete/' + itemId, this.generateHeaders());
  }

}
