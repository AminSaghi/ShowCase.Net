import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ListUsersComponent } from './list-users/list-users.component';
import { CreateEditUserComponent } from './create-edit-user/create-edit-user.component';

const routes: Routes = [
  {
    path: '',
    component: ListUsersComponent
  },
  {
    path: 'ce',
    component: CreateEditUserComponent
  },
  {
    path: 'ce/:id',
    component: CreateEditUserComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
