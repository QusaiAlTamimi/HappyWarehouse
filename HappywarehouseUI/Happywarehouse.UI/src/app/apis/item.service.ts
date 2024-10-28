import { item } from '../core/models/item';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { WarehouseSelectListItem } from '../core/models/warehouse';

@Injectable({
  providedIn: 'root'
})
export class itemService {
  private url = "Items";
  private warehouseUrl = "Warehouses";

  constructor(private http: HttpClient) { }

  public getAll() : Observable<item[]>{
    return this.http.get<item[]>(`${environment.apiUrl}/${this.url}/GetAll`);
  }

  public getPaged(pageNumber: number, pageSize: number): Observable<item[]> {
    return this.http.get<item[]>(`${environment.apiUrl}/${this.url}/GetPaged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  public WarehuseSelectList() : Observable<WarehouseSelectListItem[]>{
    return this.http.get<WarehouseSelectListItem[]>(`${environment.apiUrl}/${this.warehouseUrl}/SelectList`);
  }

  public getById(item : item) : Observable<item[]>{
    return this.http.get<item[]>(`${environment.apiUrl}/${this.url}/GetById?id=${item.id}`);
  }

  public update(item: item): Observable<item> {
    return this.http.put<item>(
      `${environment.apiUrl}/${this.url}/Update`,
      item
    );
  }

  public create(item: item): Observable<item> {
    debugger
    return this.http.post<item>(
      `${environment.apiUrl}/${this.url}/Create`,
      item
    );
  }

  public delete(id: number): Observable<item> {
    return this.http.delete<item>(
      `${environment.apiUrl}/${this.url}/Delete?id=${id}`
    );
  }
}
