import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Team } from '../interfaces/team';
import { TeamsService } from '../services/teams.service'

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})

export class TeamsComponent implements OnInit {
  teams: Team[];
  addTeam: Team;
  addTeamForm;

  constructor(private teamService: TeamsService, private formBuilder: FormBuilder) {
    this.addTeamForm = this.formBuilder.group({
      teamName: 'Name'
    });
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

  ngOnInit(): void {
    this.getTeams();

    this.teamService.refreshNeeded$
    .subscribe(() => {
      this.getTeams();
    });
  }
}
