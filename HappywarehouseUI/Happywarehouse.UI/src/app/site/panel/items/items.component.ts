import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { log } from 'console';
import { itemService } from 'src/app/apis/item.service'; // Correct the import to use camel case
import { item } from 'src/app/core/models/item';
import { PaginationDTO } from 'src/app/core/models/paginationDTO';
import { WarehouseSelectListItem } from 'src/app/core/models/warehouse';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css'],
})
export class ItemsComponent implements OnInit {
  data: item[] = []; // Full data array
  pagination: PaginationDTO<item> = new PaginationDTO<item>(); // Pagination object

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
  public warehouseSelectList: WarehouseSelectListItem[] = []; // New property for select list

  constructor(private itemService: itemService,private fb: FormBuilder) { }

  ngOnInit(): void {
    this.loadItems();
    this.getWarehouseSelectList();
  }

  loadItems(): void {
    this.itemService.getPaged(this.pagination.pageNumber, this.pagination.pageSize).subscribe({
      next: (response) => {
        if (response) {
          this.pagination = response;
          this.data = response.list;
        }
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getWarehouseSelectList(): void {
    this.itemService.WarehuseSelectList().subscribe({
      next: (response) => {
        if (response) {
          this.warehouseSelectList = response; // Assign the response to the select list
        }
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getWarehouseName(warehouseId?: number): string {
    const warehouse = this.warehouseSelectList.find(w => w.id === warehouseId);
    return warehouse ? warehouse.name : 'Unknown'; // Return 'Unknown' if not found
  }

  createItem(): void {
    if (    
      !this.newItem.name ||
      !this.newItem.skuCode  ||
      !this.newItem.qty  ||
      !this.newItem.costPrice ||
      !this.newItem.msrpPrice ||
      !this.newItem.warehouseId){
      return;
    }
    
    if (this.newItem.id === 0) {
      // Creating a new item
      this.itemService.create(this.newItem).subscribe({
        next: (item) => {
          this.data.push(item);
          this.resetForm();
        },
        error: (error) => console.log(error),
      });
    } else {
      // Editing an existing item
      this.itemService.update(this.newItem).subscribe({
        next: (updatedItem) => {
          const index = this.data.findIndex((w) => w.id === updatedItem.id);
          if (index > -1) {
            this.data[index] = updatedItem; // Update the item in the array
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
        this.data = this.data.filter((w) => w.id !== id);
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

  nextPage() {
    // Go to the next page if it's not the last page
    if (this.pagination.pageNumber < this.pagination.totalPages) {
      this.pagination.pageNumber++;
      this.loadItems();
    }
  }

  previousPage() {
    // Go to the previous page if it's not the first page
    if (this.pagination.pageNumber > 1) {
      this.pagination.pageNumber--;
      this.loadItems();
    }
  }

  setPage(page: number) {
    // Set the current page to the specified page and update pagination
    this.pagination.pageNumber = page;
    this.loadItems();
  }
}
