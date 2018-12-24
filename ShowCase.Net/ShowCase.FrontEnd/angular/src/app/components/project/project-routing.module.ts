import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ListProjectsComponent } from './list-projects/list-projects.component';
import { CreateEditProjectComponent } from './create-edit-project/create-edit-project.component';

const routes: Routes = [
  {
    path: '',
    component: ListProjectsComponent
  },
  {
    path: 'ce',
    component: CreateEditProjectComponent
  },
  {
    path: 'ce/:id',
    component: CreateEditProjectComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectRoutingModule { }
