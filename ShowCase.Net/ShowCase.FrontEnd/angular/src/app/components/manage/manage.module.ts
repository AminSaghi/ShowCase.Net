import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CardModule } from 'primeng/card';
import { FileUploadModule } from 'primeng/fileupload';
import { EditorModule } from 'primeng/editor';

import { ManageRoutingModule } from './manage-routing.module';
import { SettingsComponent } from './settings/settings.component';
import { AccountComponent } from './account/account.component';
import { ManageHomeComponent } from './manage-home/manage-home.component';

@NgModule({
  imports: [
    CommonModule,

    CardModule,
    FileUploadModule,
    EditorModule,

    ManageRoutingModule,
    SettingsComponent
  ],
  declarations: [AccountComponent, ManageHomeComponent]
})
export class ManageModule { }
