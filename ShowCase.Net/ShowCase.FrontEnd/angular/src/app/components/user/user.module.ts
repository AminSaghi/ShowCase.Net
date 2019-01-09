import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CardModule } from 'primeng/card';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { TableModule } from 'primeng/table';

import { UserRoutingModule } from './user-routing.module';
import { ListUsersComponent } from './list-users/list-users.component';
import { CreateEditUserComponent } from './create-edit-user/create-edit-user.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    CardModule,
    ConfirmDialogModule,
    TableModule,

    UserRoutingModule
  ],
  providers: [ConfirmationService],
  declarations: [ListUsersComponent, CreateEditUserComponent]
})
export class UserModule { }
