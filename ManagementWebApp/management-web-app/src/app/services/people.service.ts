import { Injectable } from '@angular/core';
import { People } from '../interfaces/people';
import { Observable, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Team } from '../interfaces/team';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {
  peopleApiUrl = 'http://localhost:80/api/PeopleModels';
  teamsApiUrl = 'http://localhost:80/api/TeamModels';
  constructor(private http: HttpClient) { }

  private _refreshNeeded$ = new Subject<void>();

  get refreshNeeded$() {
    return this._refreshNeeded$;
  }

  getPeople(): Observable<People[]> {
    return this.http.get<People[]>(this.peopleApiUrl);
  }

  addPeople(people: People): Observable<People> {
    return this.http.post<People>(this.peopleApiUrl, people)
      .pipe(
        tap(() => {
          this._refreshNeeded$.next();
        })
      );
  }

  addPersonToTeam(teamId: string, people: People): Observable<People> {
    let url = this.teamsApiUrl + "/addPeople/" + teamId;
    return this.http.post<People>(url, people)
      .pipe(
        tap(() => {
          this._refreshNeeded$.next();
        })
      );
  }
}