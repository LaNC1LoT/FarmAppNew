import { IDrug } from "./Drug";
import { IPharmacy } from "./Pharmacy";

export interface ISales {
  id: number;
  drug: IDrug;
  pharmacy: IPharmacy;
  date: string;
  price: number;
  quantity: number;
  amount: number;
  isDiscount: boolean;
  isDeleted: boolean;
}
