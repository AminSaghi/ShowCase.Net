import { Component, OnInit } from '@angular/core';

import { forkJoin } from 'rxjs';

import { ProjectService } from 'src/app/api-client';
import { Project, Page } from 'src/app/api-client/models';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent implements OnInit {

  public projects: Project[];
  public pages: Page[];

  constructor(
    private projectService: ProjectService) { }

  ngOnInit() {
    // forkJoin(
    //   this.pageService.getPages(),
    //   this.projectService.getProjects()).subscribe(results => {
    //     this.pages = results[0];
    //     this.projects = results[1];
    //   });
  }
}
