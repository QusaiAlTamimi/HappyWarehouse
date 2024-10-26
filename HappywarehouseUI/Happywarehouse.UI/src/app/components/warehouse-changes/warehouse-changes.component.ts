import { WarehouseService } from './../../services/warehouse.service';
import { warehouse } from './../../models/warehouse';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-warehouse-changes',
  templateUrl: './warehouse-changes.component.html',
  styleUrls: ['./warehouse-changes.component.css']
})
export class WarehouseChangesComponent implements OnInit {
  @Input() warehouse?: warehouse;
  @Output() warehouseUpdated = new EventEmitter<warehouse[]>();

  constructor(private WarehouseService : WarehouseService) { }

  ngOnInit(): void { }

  update(warehouse: warehouse) {
    this.WarehouseService
      .update(warehouse)
      .subscribe((warehouse: warehouse[]) => this.warehouseUpdated.emit(warehouse));
  }

  delete(warehouse: warehouse) {
    this.WarehouseService
      .delete(warehouse)
      .subscribe((warehouse: warehouse[]) => this.warehouseUpdated.emit(warehouse));
  }

  create(warehouse: warehouse) {
    this.WarehouseService
      .create(warehouse)
      .subscribe((warehouse: warehouse[]) => this.warehouseUpdated.emit(warehouse));
  }
}
