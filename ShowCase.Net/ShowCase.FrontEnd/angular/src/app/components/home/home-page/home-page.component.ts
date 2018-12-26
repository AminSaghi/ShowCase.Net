import { Component, OnInit } from '@angular/core';

import { ProjectService, FeatureService } from 'src/app/api-client';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  constructor(
    private projectService: ProjectService,
    private featureService: FeatureService) { }

  public projects = [];

  ngOnInit() {
    this.getProjects();
  }

  getProjects() {
    this.projectService.getProjects(true, false).subscribe(response => {
      this.projects = response;
    });
  }
}
