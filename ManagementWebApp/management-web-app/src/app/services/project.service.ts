import { Injectable } from '@angular/core';
import { Project } from '../interfaces/project';
import { Observable, Subject, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { tap, catchError, retry } from 'rxjs/operators';

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
        }),
        retry(1),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    window.alert('The project could not be added. Please try again later.');
    // return an observable with a user-facing error message
    return throwError(
      'The project could not be added. Please try again later.');
  };
}
