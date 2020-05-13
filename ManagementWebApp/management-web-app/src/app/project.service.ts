import { Injectable } from '@angular/core';
import { Project } from './project';
//import { PROJECTS } from './mock-projects';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  projectsApiUrl = 'https://localhost:32772/api/ProjectModels';

  getProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.projectsApiUrl);
  }

  constructor(private http: HttpClient) { }
}
