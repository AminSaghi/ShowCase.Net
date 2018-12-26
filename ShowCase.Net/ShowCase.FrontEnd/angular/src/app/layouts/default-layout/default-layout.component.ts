import { Component, OnInit } from '@angular/core';

import { forkJoin } from 'rxjs';
import { MenuItem } from 'primeng/api';

import { PageService, ProjectService } from 'src/app/api-client';
import { Page, Project } from 'src/app/api-client/models';

import { Helpers } from 'src/app/shared/helpers';

@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css']
})
export class DefaultLayoutComponent implements OnInit {

  pages: Page[];
  projects: Project[];

  public pageMenuItems: MenuItem[];
  public projectMenuItems: MenuItem[];

  constructor(
    private pageService: PageService,
    private projectService: ProjectService) { }

  ngOnInit() {
    forkJoin(
      this.pageService.getPages(),
      this.projectService.getProjects(false, true)).subscribe(results => {
        this.pages = results[0];
        this.projects = results[1];

        this.pageMenuItems = Helpers.createMenuItemsOf(this.pages, 'page');
        this.projectMenuItems = Helpers.createMenuItemsOfProjects(this.projects);
      });
  }
}
