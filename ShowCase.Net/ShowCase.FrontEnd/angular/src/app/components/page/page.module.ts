import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PageRoutingModule } from './page-routing.module';
import { ListPagesComponent } from './list-pages/list-pages.component';
import { CreateEditPageComponent } from './create-edit-page/create-edit-page.component';

@NgModule({
  imports: [
    CommonModule,
    PageRoutingModule
  ],
  declarations: [ListPagesComponent, CreateEditPageComponent]
})
export class PageModule { }
