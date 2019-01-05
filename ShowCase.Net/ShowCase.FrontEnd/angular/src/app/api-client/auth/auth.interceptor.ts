import { Injectable } from '@angular/core';
import {
    HttpEvent, HttpInterceptor, HttpHandler, HttpHeaders, HttpRequest
} from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private auth: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        request = request.clone({
            withCredentials: true,
            headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' })
        });

        if (this.auth.isAuthenticated()) {
            const authRequest = request.clone({
                setHeaders: { Authorization: this.auth.getTokenForHeader() }
            });
            return next.handle(authRequest);
        } else {
            return next.handle(request);
        }
    }

    handleError(error) {
        console.log(error);
    }
}
