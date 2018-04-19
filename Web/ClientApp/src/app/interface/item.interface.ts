import { Category } from "./category.interface";

export interface Item {
  id: string;
  orderNumber: number;
  value: string;
  description: string;
  barCode: string;
  cateogry: Category;
}
