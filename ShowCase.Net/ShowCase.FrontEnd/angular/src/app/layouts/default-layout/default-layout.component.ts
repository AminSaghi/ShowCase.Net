import { Component, OnInit } from '@angular/core';

import { forkJoin } from 'rxjs';
import { MenuItem } from 'primeng/api';

import { PageService, ProjectService } from 'src/app/api-client';
import { Page, Project } from 'src/app/api-client/models';

@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css']
})
export class DefaultLayoutComponent implements OnInit {

  public pages: Page[];
  public projects: Project[];

  constructor(
    private pageService: PageService,
    private projectService: ProjectService) { }

  ngOnInit() {
    forkJoin(
      this.pageService.getPages(),
      this.projectService.getProjects()).subscribe(results => {
        this.pages = results[0];
        this.projects = results[1];
      });
  }

}
