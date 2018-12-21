import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProjectRoutingModule } from './project-routing.module';
import { ListProjectsComponent } from './list-projects/list-projects.component';
import { CreateEditProjectComponent } from './create-edit-project/create-edit-project.component';
import { ListFeaturesComponent } from './list-features/list-features.component';

@NgModule({
  imports: [
    CommonModule,
    ProjectRoutingModule
  ],
  declarations: [ListProjectsComponent, CreateEditProjectComponent, ListFeaturesComponent]
})
export class ProjectModule { }
