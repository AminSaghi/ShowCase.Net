import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CardModule } from 'primeng/card';

import { HomeRoutingModule } from './home-routing.module';
import { HomePageComponent } from './home-page/home-page.component';
import { PageComponent } from './page/page.component';
import { FeatureComponent } from './feature/feature.component';

@NgModule({
  imports: [
    CommonModule,

    CardModule,
    
    HomeRoutingModule
  ],
  declarations: [HomePageComponent, PageComponent, FeatureComponent]
})
export class HomeModule { }
