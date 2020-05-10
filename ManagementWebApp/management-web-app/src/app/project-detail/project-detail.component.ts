import { Component, OnInit } from '@angular/core';
import { Project } from '../project';
import { Company } from '../company';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent implements OnInit {

  constructor() { }

  project: Project = {
    id: 'random GUID',
    name: 'Sample project',
    description: 'This is a random description of a project that is only mocked.',
    startDate: Date.apply("2020-03-02")
  };

  ngOnInit(): void {
  }

}
