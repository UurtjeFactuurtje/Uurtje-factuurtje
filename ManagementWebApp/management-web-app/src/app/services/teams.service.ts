import { Injectable } from '@angular/core';
import { Team } from '../interfaces/team';
import { Observable, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class TeamsService {
  constructor(private http: HttpClient) { }

  private _refreshNeeded$ = new Subject<void>();

  get refreshNeeded$() {
    return this._refreshNeeded$;
  }

  teamsApiUrl = 'http://localhost:80/api/TeamModels';

  getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(this.teamsApiUrl);
  }

  addTeam(team: Team): Observable<Team> {
    return this.http.post<Team>(this.teamsApiUrl, team)
      .pipe(
        tap(() => {
          this._refreshNeeded$.next();
        })
      );
  }
}