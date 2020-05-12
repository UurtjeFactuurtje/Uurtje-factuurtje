import { Injectable } from '@angular/core';
import { Project } from './project';
import { PROJECTS } from './mock-projects';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  getProjects(): Observable<Project[]> {
    return of (PROJECTS);
  }

  constructor(private http: HttpClient) { }
}
