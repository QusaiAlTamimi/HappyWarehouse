import { Component, OnInit } from '@angular/core';
import { WarehouseService } from 'src/app/apis/warehouse.service';
import { warehouse } from 'src/app/core/models/warehouse';

@Component({
  selector: 'app-werahouses',
  templateUrl: './werahouses.component.html',
  styleUrls: ['./werahouses.component.css']
})
export class WerahousesComponent implements OnInit {
  public warehouses: warehouse[] = [];

  constructor(private warehouseService: WarehouseService) { }

  ngOnInit(): void {
    this.warehouseService.getAll().subscribe({
      next: response => {
        if (response) {
          this.warehouses = response;
        }
      },
      error: error => {
        console.log(error);
      }
    });
  }

}
