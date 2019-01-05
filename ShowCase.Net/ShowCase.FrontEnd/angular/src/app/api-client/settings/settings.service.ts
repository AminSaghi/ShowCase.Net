import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Settings } from '../models';
import { Urls } from '../urls';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private http: HttpClient) { }

  getSettings(): Observable<Settings> {
    return this.http.get<Settings>(Urls.SETTINGS);
  }

  editSettings(settings: Settings): Observable<Settings> {
    return this.http.put<Settings>(Urls.SETTINGS, Settings);
  }
}
