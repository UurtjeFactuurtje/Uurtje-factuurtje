import { Injectable } from '@angular/core';
import { Project } from '../interfaces/project';
import { Observable, of, Subject } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {tap} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  constructor(private http: HttpClient) { }

  private _refreshNeeded$ = new Subject<void>();

  get refreshNeeded$() {
    return this._refreshNeeded$;
  }

  projectsApiUrl = 'http://localhost:80/api/ProjectModels';

  getProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.projectsApiUrl);
  }

  addProject(project: Project): Observable<Project> {
    return this.http.post<Project>(this.projectsApiUrl, project)
      .pipe(
        tap(() => {
          this._refreshNeeded$.next();
        })
      );
  }
}
