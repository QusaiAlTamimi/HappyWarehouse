import { item } from "./item";

export class warehouse {
  id?: number;
  name = "";
  address = "";
  city = "";
  country?: number;
  items: item[] = [];
}