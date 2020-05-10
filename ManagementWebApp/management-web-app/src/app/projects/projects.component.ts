import { Component, OnInit } from '@angular/core';
import {MatGridListModule} from '@angular/material/grid-list';
import { PROJECTS } from '../mock-projects';
import { Project } from '../project';
import { Company } from '../company';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  constructor() { }

  projects = PROJECTS;

  ngOnInit(): void {
  }

}
