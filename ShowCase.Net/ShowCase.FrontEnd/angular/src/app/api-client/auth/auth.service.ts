import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Urls } from '../urls';
import { ChangePasswordApiModel } from '../models/api-models/change-password-api-model';

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
