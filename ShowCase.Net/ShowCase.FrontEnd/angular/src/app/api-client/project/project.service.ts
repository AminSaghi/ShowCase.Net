import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Urls } from '../urls';

import { Project } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private http: HttpClient) { }

  getProjects(withImage = false, withFeatures = false): Observable<Project[]> {
    const url = `${Urls.PROJECTS}?${withImage ? 'image=1&' : ''}${withFeatures ? 'features=1' : ''}`;
    return this.http.get<Project[]>(url);
  }

  getProject(id: number): Observable<Project> {
    const url = `${Urls.PROJECTS}${id}`;
    return this.http.get<Project>(url);
  }

  createProject(project: Project): Observable<any> {
    return this.http.post<any>(Urls.PROJECTS, project);
  }

  editProject(project: Project): Observable<Project> {
    return this.http.put<Project>(Urls.PROJECTS, project);
  }

  deleteProject(id: number): Observable<any> {
    const url = `${Urls.PROJECTS}${id}`;
    return this.http.delete<Project>(url);
  }
}
