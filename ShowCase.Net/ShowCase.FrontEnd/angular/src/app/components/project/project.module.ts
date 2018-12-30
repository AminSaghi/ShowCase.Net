import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CardModule } from 'primeng/card';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { FileUploadModule } from 'primeng/fileupload';
import { SpinnerModule } from 'primeng/spinner';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { EditorModule } from 'primeng/editor';

import { ProjectRoutingModule } from './project-routing.module';
import { ListProjectsComponent } from './list-projects/list-projects.component';
import { CreateEditProjectComponent } from './create-edit-project/create-edit-project.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    CardModule,
    ConfirmDialogModule,
    TableModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    FileUploadModule,
    SpinnerModule,
    ToggleButtonModule,
    EditorModule,

    ProjectRoutingModule
  ],
  providers: [ConfirmationService],
  declarations: [ListProjectsComponent, CreateEditProjectComponent]
})
export class ProjectModule { }
