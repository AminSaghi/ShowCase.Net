import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Urls } from '../urls';

import { User } from '../models';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(Urls.USERS);
  }

  getUser(id: number): Observable<User> {
    const url = `${Urls.USERS}/${id}`;
    return this.http.get<User>(url);
  }

  createUser(user: User): Observable<any> {
    return this.http.post<any>(Urls.USERS, user);
  }

  editUser(user: User): Observable<User> {
    return this.http.put<User>(Urls.USERS, user);
  }

  deleteUser(id: string): Observable<any> {
    const url = `${Urls.USERS}/${id}`;
    return this.http.delete<User>(url);
  }
}
