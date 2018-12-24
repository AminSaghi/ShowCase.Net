import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ConfirmationService } from 'primeng/api';

import { ProjectService } from 'src/app/api-client';

@Component({
  selector: 'app-list-projects',
  templateUrl: './list-projects.component.html',
  styleUrls: ['./list-projects.component.css']
})
export class ListProjectsComponent implements OnInit {

  constructor(
    private projectService: ProjectService,
    private router: Router,
    private confirmationService: ConfirmationService) { }

  public projects = [];

  ngOnInit() {
    this.getProjects();
  }

  getProjects() {
    this.projectService.getProjects().subscribe(response => {
      this.projects = response;
    });
  }

  confirmDelete(id: number) {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to delete this Project?',
      accept: () => {
        this.deleteProject(id);
      }
    });
  }

  deleteProject(projectId: number) {
    this.projectService.deleteProject(projectId).subscribe(() => {
      this.projects = this.projects.filter(i => i.id !== projectId);
    });
  }

}
