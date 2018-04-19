import { Category } from "./category.interface";

export interface Item {
  inventoryItemId: string;
  orderNumber: number;
  value: string;
  description: string;
  barCode: string;
  category: Category;
}
