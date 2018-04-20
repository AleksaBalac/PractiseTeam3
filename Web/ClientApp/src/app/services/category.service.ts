import { Injectable } from '@angular/core';
import { ServiceHelper } from './service.helper';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { Category } from '../interface/category.interface';

@Injectable()
export class CategoryService extends ServiceHelper {

  constructor(
    private http: HttpClient, private router: Router, public snackBar: MatSnackBar) {
    super(snackBar);
  }

  getCategories() {
    return this.http.get(this.apiAddress + '/category/list', this.generateHeadersWithToken());
  }

  addCategory(category: string) {
    let categoryViewModel: Category = <Category>{
      categoryId: '',
      name: category
    }
    return this.http.post(this.apiAddress + '/category/add', categoryViewModel, this.generateHeadersWithToken());
  }

  updateCategory(category: Category) {
    return this.http.put(this.apiAddress + '/category/update', category, this.generateHeaders());
  }

  deleteCategory(categoryId: string) {
    return this.http.delete(this.apiAddress + '/category/delete/' + categoryId, this.generateHeaders());
  }

}
