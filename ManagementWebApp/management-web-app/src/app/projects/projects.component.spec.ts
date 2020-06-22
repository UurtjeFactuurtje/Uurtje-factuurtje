import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ProjectsComponent } from './projects.component';
import { DebugElement } from '@angular/core';
import { Project } from '../interfaces/project';
import { ProjectService } from '../services/project.service';
import { of } from 'rxjs';

describe('ProjectsComponent', () => {
  let component: ProjectsComponent;
  let fixture: ComponentFixture<ProjectsComponent>;
  let de: DebugElement;

  let projectServiceStub: any;

  beforeEach(async(() => {
    projectServiceStub = {
      onSubmit: () => of('Blablabla'),
      getProjects: () => of(Project)
    };

    TestBed.configureTestingModule({
      declarations: [ProjectsComponent],
      //providers: [{ provide: ProjectService, useValue: projectServiceStub }]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectsComponent);
    component = fixture.componentInstance;
    de = fixture.debugElement;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a form to add a project', () => {
    expect(component.addProjectForm).toBeTruthy();
  });

  it('should add a project when the form is submitted', () => {
    //component.addProject = new Project();
    //component.onSubmit(component.addProject);
  });
});


