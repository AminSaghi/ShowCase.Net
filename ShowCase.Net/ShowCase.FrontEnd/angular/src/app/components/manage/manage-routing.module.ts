import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ManageHomeComponent } from './manage-home/manage-home.component';
import { SettingsComponent } from './settings/settings.component';
import { AccountComponent } from './account/account.component';

const routes: Routes = [
  {
    path: '',
    component: ManageHomeComponent
  },
  {
    path: 'settings',
    component: SettingsComponent
  },
  {
    path: 'account',
    component: AccountComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManageRoutingModule { }
