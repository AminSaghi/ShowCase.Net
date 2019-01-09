import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Urls } from '../urls';
import { ChangePasswordApiModel } from '../models/api-models/change-password-api-model';
import { User } from '../models';

@Injectable()
export class AuthService {

    constructor(private http: HttpClient) { }

    login(loginModel) {
        return this.http.post(Urls.LOGIN, loginModel);
    }

    logout() {
        localStorage.removeItem('token');
    }

    changePassword(changePassModel: ChangePasswordApiModel): Observable<ChangePasswordApiModel> {
        return this.http.post<ChangePasswordApiModel>(Urls.CHANGE_PASSWORD, changePassModel);
    }

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

    isAuthenticated() {
        return !!localStorage.getItem('token');
    }

    getTokenForHeader() {
        return 'Bearer ' + localStorage.getItem('token');
    }

    setAuthToken(token) {
        localStorage.setItem('token', token);
    }
}
