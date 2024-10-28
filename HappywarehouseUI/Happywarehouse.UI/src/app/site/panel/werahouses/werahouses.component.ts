import { Component, OnInit } from '@angular/core';
import { WarehouseService } from 'src/app/apis/warehouse.service';
import { Country } from 'src/app/core/models/countries.enum';
import { PaginationDTO } from 'src/app/core/models/paginationDTO';
import { warehouse } from 'src/app/core/models/warehouse';

@Component({
  selector: 'app-werahouses',
  templateUrl: './werahouses.component.html',
  styleUrls: ['./werahouses.component.css']
})
export class WerahousesComponent implements OnInit {
  data: warehouse[] = []; // Full data array
  pagination: PaginationDTO<warehouse> = new PaginationDTO<warehouse>(); // Pagination object
  public newWarehouse: warehouse = { id: 0, name: '', address: '', city: '', country: 0, items: [] };
  public showForm = false;
  public editWarehouseId: number | null | undefined = null;
  public countries: { id: number; name: string }[] = [];

  constructor(private warehouseService: WarehouseService) { }

  ngOnInit(): void {
    this.loadWarehouses();
    this.countries = this.getCountriesList(); // Initialize countries here
  }

  getCountriesList() {
    return Object.keys(Country)
      .filter(key => isNaN(Number(key))) // Filter out numeric enum keys
      .map(key => ({
        id: Country[key as keyof typeof Country],
        name: key.replace(/([A-Z])/g, ' $1').trim()
      }));
  }

  getCountryName(countryId?: number): string {
    if (countryId !== undefined && countryId in Country) {
      const countryName = Country[countryId.toString() as keyof typeof Country];
      return typeof countryName === 'string' ? countryName : String(countryName);
    }
    return 'Unknown'; // Handle undefined or invalid IDs
  }

  loadWarehouses(): void {
    this.warehouseService.getPaged(this.pagination.pageNumber, this.pagination.pageSize).subscribe({
      next: response => {
        if (response) {
          this.pagination = response;
          this.data = response.list;
        }
      },
      error: error => {
        console.log(error);
      }
    });
  }

  createWarehouse(): void {
    if (this.newWarehouse.id === 0) {
      // Creating a new warehouse
      this.warehouseService.create(this.newWarehouse).subscribe({
        next: (warehouse) => {
          this.data.push(warehouse);
          this.resetForm();
        },
        error: (error) => console.log(error),
      });
    } else {
      // Editing an existing warehouse
      this.warehouseService.update(this.newWarehouse).subscribe({
        next: (updatedWarehouse) => {
          const index = this.data.findIndex(w => w.id === updatedWarehouse.id);
          if (index > -1) {
            this.data[index] = updatedWarehouse; // Update the warehouse in the array
          }
          this.resetForm();
        },
        error: (error) => console.log(error),
      });
    }
  }

  editWarehouse(warehouse: warehouse): void {
    this.newWarehouse = { ...warehouse }; // Copy the warehouse data into newWarehouse
    this.showForm = true; // Show the form for editing
    this.editWarehouseId = warehouse.id; // Set the id for tracking
  }

  deleteWarehouse(id: number): void {
    this.warehouseService.delete(id).subscribe({
      next: () => {
        this.data = this.data.filter(w => w.id !== id);
      },
      error: (error) => console.log(error),
    });
  }

  resetForm(): void {
    this.newWarehouse = { id: 0, name: '', address: '', city: '', country: 0, items: [] };
    this.showForm = false; // Hide the form after submission
    this.editWarehouseId = null; // Reset the edit ID
  }


  nextPage() {
    // Go to the next page if it's not the last page
    if (this.pagination.pageNumber < this.pagination.totalPages) {
      this.pagination.pageNumber++;
      this.loadWarehouses();
    }
  }

  previousPage() {
    // Go to the previous page if it's not the first page
    if (this.pagination.pageNumber > 1) {
      this.pagination.pageNumber--;
      this.loadWarehouses();
    }
  }

  setPage(page: number) {
    // Set the current page to the specified page and update pagination
    this.pagination.pageNumber = page;
    this.loadWarehouses();
  }
}
