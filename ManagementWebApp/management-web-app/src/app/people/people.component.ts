import { Component, OnInit } from '@angular/core';
import { PeopleService } from '../services/people.service';
import { People } from '../interfaces/people';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.css']
})
export class PeopleComponent implements OnInit {

  peoples: People[];
  columnsToDisplay = ['firstName', 'lastName', 'Id'];

  constructor(private peopleService: PeopleService) {
   }

  getPeople(): void {
    this.peopleService.getPeople()
    .subscribe(peoples => this.peoples = peoples);
  }

  ngOnInit(): void {
    this.getPeople();
  }
}
