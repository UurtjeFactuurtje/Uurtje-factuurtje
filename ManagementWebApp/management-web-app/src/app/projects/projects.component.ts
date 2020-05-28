import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../services/project.service';
import { Project } from '../interfaces/project';
import { FormBuilder, FormControl } from '@angular/forms';

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
      projectStartDate: '2020-05-14T00:00',
      projectEndDate: '2020-05-14T00:00'
    });
   }

  getProjects(): void {
    this.projectService.getProjects()
      .subscribe(projects => this.projects = projects);
  }
  
  onSubmit(projectInfo) {
    this.addProject = new Project();;
    console.warn("Before", this.addProject);

    this.addProject.Name = projectInfo.projectName;
    this.addProject.Description = projectInfo.projectDescription;
    this.addProject.StartDate = projectInfo.projectStartDate;
    this.addProject.EndDate = projectInfo.projectEndDate;
    this.projectService.addProject(this.addProject).subscribe(res => console.log(res));
    this.refresh();
  }

  refresh(): void {
    window.location.reload();
}

  ngOnInit(): void {
    this.getProjects();
  }

}
