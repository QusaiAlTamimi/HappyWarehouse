import { warehouse } from '../core/models/warehouse';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {
  private url = "Warehouses";
  constructor(private http: HttpClient) { }

  public getAll() : Observable<warehouse[]>{
    return this.http.get<warehouse[]>(`${environment.apiUrl}/${this.url}/GetAll`);
  }

  public getById(warehouse : warehouse) : Observable<warehouse[]>{
    return this.http.get<warehouse[]>(`${environment.apiUrl}/${this.url}/GetById/${warehouse.id}`);
  }

  public update(warehouse: warehouse): Observable<warehouse> {
    return this.http.put<warehouse>(
      `${environment.apiUrl}/${this.url}/Update/`,
      warehouse
    );
  }

  public create(warehouse: warehouse): Observable<warehouse> {
    return this.http.post<warehouse>(
      `${environment.apiUrl}/${this.url}/Create`,
      warehouse
    );
  }

  public delete(id: number): Observable<void> {
    return this.http.delete<void>(
      `${environment.apiUrl}/${this.url}/Delete?id=${id}`
    );
  }
}
