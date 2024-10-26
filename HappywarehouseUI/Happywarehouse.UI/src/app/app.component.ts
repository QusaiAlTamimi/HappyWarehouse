import { WarehouseService } from './services/warehouse.service';
import { warehouse } from './models/warehouse';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Happywarehouse.UI';
  warehouses? : warehouse[] =[];
  warehouseToEdit?: warehouse;
  constructor(private WarehouseService : WarehouseService) {}

  ngOnInit() : void {
    this.WarehouseService.
    getAll()
    .subscribe((result: warehouse[]) => (this.warehouses = result));
  }

  updateList(warehouses: warehouse[]) {
    this.warehouses = warehouses;
  }

  initWarehouse() {
    this.warehouseToEdit = new warehouse();
  }

  editWarehouse(warehouse: warehouse) {
    this.warehouseToEdit = warehouse;
  }
}
