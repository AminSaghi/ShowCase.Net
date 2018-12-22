import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Urls } from '../urls';

@Injectable()
export class AuthService {

    constructor(private http: HttpClient) { }

    isAuthenticated() {
        return !!localStorage.getItem('token');
    }

    getTokenForHeader() {
        return 'Bearer ' + localStorage.getItem('token');
    }

    login(loginModel) {
        return this.http.post(Urls.LOGIN, loginModel);
    }

    logout() {
        localStorage.removeItem('token');
    }

    setAuthToken(token) {
        localStorage.setItem('token', token);
    }
}
