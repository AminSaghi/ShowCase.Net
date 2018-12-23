import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ListFeaturesComponent } from './list-features/list-features.component';
import { CreateEditFeatureComponent } from './create-edit-feature/create-edit-feature.component';

const routes: Routes = [
  {
    path: '',
    component: ListFeaturesComponent
  },
  {
    path: 'ce',
    component: CreateEditFeatureComponent
  },
  {
    path: 'ce/:id',
    component: CreateEditFeatureComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FeatureRoutingModule { }
