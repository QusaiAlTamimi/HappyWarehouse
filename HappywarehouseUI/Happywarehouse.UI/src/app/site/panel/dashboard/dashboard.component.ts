import { itemService } from 'src/app/apis/item.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WarehouseService } from 'src/app/apis/warehouse.service';
import { warehouse } from 'src/app/core/models/warehouse';
import { AuthService } from 'src/app/core/services/authService';
import { item } from 'src/app/core/models/item';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  public warehouses: warehouse[] = [];
  public items: item[] = [];
  public itemsAsc: item[] = []; // For items sorted in ascending order
  public itemsDesc: item[] = []; // For items sorted in descending order

  constructor(private warehouseService: WarehouseService,private itemService: itemService) { }

  ngOnInit(): void {
    this.loadWarehouses();
    this.loadItems();
  }

  loadWarehouses(): void {
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
  loadItems(): void {
    this.itemService.getAll().subscribe({
      next: (response) => {
        if (response) {
          this.items = response;
          // Sort items into ascending and descending orders
          const validItems = this.items.filter(item => item.qty !== null);

          this.itemsAsc = [...validItems].sort((a, b) => a.qty! - b.qty!); // Use non-null assertion
          this.itemsDesc = [...validItems].sort((a, b) => b.qty! - a.qty!); // Use non-null assertion
          console.log(this.items);
        }
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
