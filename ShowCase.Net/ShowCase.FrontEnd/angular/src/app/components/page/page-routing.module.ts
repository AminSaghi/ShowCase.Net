import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ListPagesComponent } from './list-pages/list-pages.component';
import { CreateEditPageComponent } from './create-edit-page/create-edit-page.component';

const routes: Routes = [
  {
    path: '',
    component: ListPagesComponent
  },
  {
    path: 'ce',
    component: CreateEditPageComponent
  },
  {
    path: 'ce/:id',
    component: CreateEditPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PageRoutingModule { }
