import { Injectable } from '@angular/core';
import {
    HttpEvent, HttpInterceptor, HttpHandler, HttpHeaders, HttpRequest, HttpErrorResponse
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { MessageService } from 'primeng/api';

import { AuthService } from './auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private auth: AuthService,
        private router: Router,
        private messageService: MessageService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        request = request.clone({
            withCredentials: true,
            headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' })
        });

        if (this.auth.isAuthenticated()) {
            const authRequest = request.clone({
                setHeaders: { Authorization: this.auth.getTokenForHeader() }
            });
            return next.handle(authRequest).pipe(
                tap(event => { },
                    error => { this.handleError(error); }
                ));
        } else {
            return next.handle(request).pipe(
                tap(event => { },
                    error => { this.handleError(error); }
                ));
        }
    }

    handleError(error) {
        console.log(error);
        let title = 'Error';

        if (error instanceof HttpErrorResponse) {
            if (error.status === 401) {
                this.auth.logout();
                this.router.navigate(['/not-found']);
            }

            if (error.status >= 400 && error.status < 500) {
                title = 'Client error';
            }

            if (error.status === 500) {
                title = 'Runtime error';
            }
        }

        this.messageService.add({
            severity: 'error',
            summary: title,
            detail: error.error
        });
    }
}
