import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { Team } from '../interfaces/team';
import { TeamsService } from '../services/teams.service';
import { PeopleService } from '../services/people.service';
import { People } from '../interfaces/people';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})

export class TeamsComponent implements OnInit {
  teams: Team[];
  addTeam: Team;
  addTeamForm;
  selectedTeam:Team;
  selectedPerson:People;
  peopleList: People[];
  selected = 'option2';


  constructor(private teamService: TeamsService, private peopleService: PeopleService, private formBuilder: FormBuilder) {
    this.addTeamForm = this.formBuilder.group({
      teamName: 'Name'
    });
   }

   getPeople():void{
     this.peopleService.getPeople()
     .subscribe(people => this.peopleList = people);
   }

  getTeams(): void {
    this.teamService.getTeams()
    .subscribe(teams => this.teams = teams);
  }
  
  onSubmit(teamInfo) {
    this.addTeam = new Team();
    this.addTeam.Name = teamInfo.teamName;

    this.teamService.addTeam(this.addTeam).subscribe(res => console.log(res));
  }

  addPersonToTeam(teamId, peopleId)
  {
    this.selectedTeam = this.teams.find(teamId);
    this.selectedPerson = this.peopleList.find(peopleId);
    this.peopleService.addPersonToTeam(this.selectedTeam.Id, this.selectedPerson).subscribe(res => console.log(res));
  }

  ngOnInit(): void {
    this.getTeams();

    this.getPeople();

    this.teamService.refreshNeeded$
    .subscribe(() => {
      this.getTeams();
    });
  }
}
