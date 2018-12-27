import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { TreeTableModule } from 'primeng/treetable';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { SpinnerModule } from 'primeng/spinner';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { EditorModule } from 'primeng/editor';

import { PageRoutingModule } from './page-routing.module';
import { ListPagesComponent } from './list-pages/list-pages.component';
import { CreateEditPageComponent } from './create-edit-page/create-edit-page.component';

@NgModule({
  imports: [
    CommonModule,

    FormsModule,
    ReactiveFormsModule,

    ConfirmDialogModule,
    TreeTableModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    SpinnerModule,
    ToggleButtonModule,
    EditorModule,

    PageRoutingModule
  ],
  providers: [ConfirmationService],
  declarations: [ListPagesComponent, CreateEditPageComponent]
})
export class PageModule { }
