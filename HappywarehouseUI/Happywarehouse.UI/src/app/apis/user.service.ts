import { ChangePassword } from './../core/models/user';
import { LoginRequest, LoginResponse, user } from '../core/models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class userService {
  private url = 'Auth';
  constructor(private http: HttpClient) {}

  public getAll(): Observable<user[]> {
    return this.http.get<user[]>(`${environment.apiUrl}/${this.url}/GetAll`);
  }

  public getPaged(pageNumber: number, pageSize: number): Observable<user[]> {
    return this.http.get<user[]>(`${environment.apiUrl}/${this.url}/GetPaged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  public getById(user: user): Observable<user[]> {
    return this.http.get<user[]>(
      `${environment.apiUrl}/${this.url}/GetById/${user.id}`
    );
  }

  public login(userLogin: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${environment.apiUrl}/${this.url}/Login`,
      userLogin
    );
  }

  public create(user: user): Observable<user> {
    return this.http.post<user>(
      `${environment.apiUrl}/${this.url}/Create`,
      user
    );
  }

  public update(userId: string, user: user): Observable<user> {
    return this.http.put<user>(
      `${environment.apiUrl}/${this.url}/Update?userId=${userId}`,
      user
    );
  }

  public changePassword(userId: string, ChangePassword: ChangePassword): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${environment.apiUrl}/${this.url}/ChangePassword?userId=${userId}`,
      ChangePassword
    );
  }

  public delete(id: string): Observable<user> {
    return this.http.delete<user>(
      `${environment.apiUrl}/${this.url}/DeleteUser?userId=${id}`
    );
  }
}
