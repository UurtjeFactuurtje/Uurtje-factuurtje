import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../services/project.service';
import { Project } from '../interfaces/project';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  projects: Project[];
  addProject: Project;
  addProjectForm;

  constructor(private projectService: ProjectService, private formBuilder: FormBuilder) {
    this.addProjectForm = this.formBuilder.group({
      projectName: 'Name',
      projectDescription: 'Description',
      projectStartDate: '',
      projectEndDate: ''
    });
   }

  getProjects(): void {
    this.projectService.getProjects()
      .subscribe(projects => this.projects = projects);
  }
  
  onSubmit(projectInfo) {
    this.addProject = new Project();

    this.addProject.Name = projectInfo.projectName;
    this.addProject.Description = projectInfo.projectDescription;
    this.addProject.StartDate = projectInfo.projectStartDate;
    this.addProject.EndDate = projectInfo.projectEndDate;
    this.projectService.addProject(this.addProject).subscribe(res => console.log(res));
  }

  ngOnInit(): void {
    this.getProjects();

    this.projectService.refreshNeeded$
    .subscribe(() => {
      this.getProjects();
    });
  }
}
