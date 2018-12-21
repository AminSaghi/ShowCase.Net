import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Urls } from '../urls';

import { Page } from '../models';

@Injectable({
  providedIn: 'root'
})
export class PageService {

  constructor(private http: HttpClient) { }

  getPages(): Observable<Page[]> {
    return this.http.get<Page[]>(Urls.PAGES);
  }

  getPage(id: number): Observable<Page> {
    const url = `${Urls.PAGES}${id}`;
    return this.http.get<Page>(url);
  }

  createPage(page: Page): Observable<any> {
    return this.http.post<any>(Urls.PAGES, page);
  }

  editPage(page: Page): Observable<Page> {
    return this.http.put<Page>(Urls.PAGES, page);
  }

  deletePage(id: number): Observable<any> {
    const url = `${Urls.PAGES}${id}`;
    return this.http.delete<Page>(url);
  }
}
