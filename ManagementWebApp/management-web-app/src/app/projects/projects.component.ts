import { Component, OnInit } from '@angular/core';
import {MatGridListModule} from '@angular/material/grid-list';
import { ProjectService } from '../project.service';
import { Project } from '../project';
import { Company } from '../company';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  constructor(private projectService: ProjectService) { }

  projects : Project[];

  getProjects(): void{
    this.projectService.getProjects()
    .subscribe(projects => this.projects = projects);
  }

  ngOnInit(): void {
    this.getProjects();
  }

}
