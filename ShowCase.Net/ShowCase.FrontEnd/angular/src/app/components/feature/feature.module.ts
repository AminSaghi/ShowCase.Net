import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TreeTableModule } from 'primeng/treetable';
import { ButtonModule } from 'primeng/button';

import { FeatureRoutingModule } from './feature-routing.module';
import { CreateEditFeatureComponent } from './create-edit-feature/create-edit-feature.component';
import { ListFeaturesComponent } from './list-features/list-features.component';

@NgModule({
  imports: [
    CommonModule,

    ConfirmDialogModule,
    TreeTableModule,
    ButtonModule,

    FeatureRoutingModule
  ],
  declarations: [CreateEditFeatureComponent, ListFeaturesComponent]
})
export class FeatureModule { }
