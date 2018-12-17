
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Urls } from '../urls';

import { Feature } from '../interfaces';

@Injectable()
export class FeatureService {

  constructor(private http: HttpClient) { }

  getFeatures(): Observable<Feature[]> {
    return this.http.get<Feature[]>(Urls.FEATURES);
  }

  getFeature(id: number): Observable<Feature> {
    const url = `${Urls.FEATURES}${id}`;
    return this.http.get<Feature>(url);
  }

  createFeature(feature: Feature): Observable<any> {
    return this.http.post<any>(Urls.FEATURES, feature);
  }

  editFeature(feature: Feature): Observable<Feature> {
    return this.http.put<Feature>(Urls.FEATURES, feature);
  }

  deleteFeature(id: number): Observable<any> {
    const url = `${Urls.FEATURES}${id}`;
    return this.http.delete<Feature>(url);
  }
}
