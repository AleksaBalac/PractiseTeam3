import { Category } from "./category.interface";

export interface Item {
  inventoryItemId: string;
  name:string,
  orderNumber: number;
  value: string;
  description: string;
  barCode: string;
  category: Category;
}
