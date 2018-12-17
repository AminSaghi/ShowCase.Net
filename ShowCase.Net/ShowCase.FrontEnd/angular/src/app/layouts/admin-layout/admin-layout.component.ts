import { Component, OnInit } from '@angular/core';

import { forkJoin } from 'rxjs';

import { ProjectService } from 'src/app/api-client';
import { Project } from 'src/app/api-client/models';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent implements OnInit {

  public projects: Project[];

  constructor(
    private projectService: ProjectService) { }

  ngOnInit() {
    const that = this;

    this.projectService.getProjects().subscribe(result => {
      this.projects = result;
    });
  }

}
