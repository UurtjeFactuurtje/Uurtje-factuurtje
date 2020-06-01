import { Injectable } from '@angular/core';
import { People } from '../interfaces/people';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {
  peopleApiUrl = 'http://localhost:80/api/PeopleModels';

  getPeople(): Observable<People[]> {
    return this.http.get<People[]>(this.peopleApiUrl);
  }

  addPerson (person: People): Observable<People> {
    return this.http.post<People>(this.peopleApiUrl, person);
  }

  constructor(private http: HttpClient) { }
}
