import { LoginRequest, LoginResponse, user } from '../core/models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class userService {
  private url = "Auth";
  constructor(private http: HttpClient) { }

  public getAll() : Observable<user[]>{
    return this.http.get<user[]>(`${environment.apiUrl}/${this.url}/GetAll`);
  }

  public getById(user : user) : Observable<user[]>{
    return this.http.get<user[]>(`${environment.apiUrl}/${this.url}/GetById/${user.id}`);
  }

  public login(userLogin: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${environment.apiUrl}/${this.url}/Login`,
      userLogin
    );
  }

  public create(user: user): Observable<user[]> {
    return this.http.post<user[]>(
      `${environment.apiUrl}/${this.url}/Create`,
      user
    );
  }

  public delete(user: user): Observable<user[]> {
    return this.http.delete<user[]>(
      `${environment.apiUrl}/${this.url}/DeleteUser?userId=${user.id}`
    );
  }
}
