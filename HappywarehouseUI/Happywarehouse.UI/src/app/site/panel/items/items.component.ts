import { Component, OnInit } from '@angular/core';
import { log } from 'console';
import { itemService } from 'src/app/apis/item.service'; // Correct the import to use camel case
import { item } from 'src/app/core/models/item';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css'],
})
export class ItemsComponent implements OnInit {
  public items: item[] = [];
  public newItem: item = {
    id: 0,
    name: '',
    skuCode: '',
    msrpPrice: 0,
    qty: 0,
    costPrice: 0,
    warehouseId: 0,
  };
  public showItemForm = false; // Changed to be more descriptive
  public editItemId: number | null | undefined = null; // Track the id of the item being edited

  constructor(private itemService: itemService) {}

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems(): void {
    this.itemService.getAll().subscribe({
      next: (response) => {
        if (response) {
          this.items = response;
          console.log(this.items);
        }
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  createItem(): void {
    if (this.newItem.id === 0) {
      // Creating a new item
      this.itemService.create(this.newItem).subscribe({
        next: (item) => {
          this.items.push(item);
          this.resetForm();
        },
        error: (error) => console.log(error),
      });
    } else {
      // Editing an existing item
      this.itemService.update(this.newItem).subscribe({
        next: (updatedItem) => {
          const index = this.items.findIndex((w) => w.id === updatedItem.id);
          if (index > -1) {
            this.items[index] = updatedItem; // Update the item in the array
          }
          this.resetForm();
        },
        error: (error) => console.log(error),
      });
    }
  }

  editItem(item: item): void {
    this.newItem = { ...item }; // Copy the item data into newItem
    this.showItemForm = true; // Show the form for editing
    this.editItemId = item.id; // Set the id for tracking
  }

  deleteItem(id: number): void {
    this.itemService.delete(id).subscribe({
      next: () => {
        this.items = this.items.filter((w) => w.id !== id);
      },
      error: (error) => console.log(error),
    });
  }

  resetForm(): void {
    this.newItem = {
      id: 0,
      name: '',
      skuCode: '',
      msrpPrice: 0,
      qty: 0,
      costPrice: 0,
      warehouseId: 0,
    };
    this.showItemForm = false; // Hide the form after submission
    this.editItemId = null; // Reset the edit ID
  }
}
