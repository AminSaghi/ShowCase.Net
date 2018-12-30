import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CardModule } from 'primeng/card';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { TreeTableModule } from 'primeng/treetable';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { SpinnerModule } from 'primeng/spinner';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { EditorModule } from 'primeng/editor';

import { FeatureRoutingModule } from './feature-routing.module';
import { CreateEditFeatureComponent } from './create-edit-feature/create-edit-feature.component';
import { ListFeaturesComponent } from './list-features/list-features.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    CardModule,
    ConfirmDialogModule,
    TreeTableModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    SpinnerModule,
    ToggleButtonModule,
    EditorModule,

    FeatureRoutingModule
  ],
  providers: [ConfirmationService],
  declarations: [CreateEditFeatureComponent, ListFeaturesComponent]
})
export class FeatureModule { }
