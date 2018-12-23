import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

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

    InputTextModule,
    SpinnerModule,
    ToggleButtonModule,
    EditorModule,

    PageRoutingModule
  ],
  declarations: [ListPagesComponent, CreateEditPageComponent]
})
export class PageModule { }
