import { item } from '../core/models/item';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class itemService {
  private url = "Items";
  constructor(private http: HttpClient) { }

  public getAll() : Observable<item[]>{
    return this.http.get<item[]>(`${environment.apiUrl}/${this.url}/GetAll`);
  }

  public getById(item : item) : Observable<item[]>{
    return this.http.get<item[]>(`${environment.apiUrl}/${this.url}/GetById?id=${item.id}`);
  }

  public update(item: item): Observable<item[]> {
    return this.http.put<item[]>(
      `${environment.apiUrl}/${this.url}/Update`,
      item
    );
  }

  public create(item: item): Observable<item[]> {
    debugger
    return this.http.post<item[]>(
      `${environment.apiUrl}/${this.url}/Create`,
      item
    );
  }

  public delete(item: item): Observable<item[]> {
    return this.http.delete<item[]>(
      `${environment.apiUrl}/${this.url}/Delete?id=${item.id}`
    );
  }
}
