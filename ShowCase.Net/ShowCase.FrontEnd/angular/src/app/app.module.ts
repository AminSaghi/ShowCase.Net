import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app.routing';
import { ApiClientModule } from './api-client';

import { AppComponent } from './app.component';
import { DefaultLayoutComponent } from './layouts/default-layout/default-layout.component';
import { DefaultLayoutGlobals } from './layouts/default-layout/default-layout-globals';
import { LoginComponent } from './components/login/login.component';
import { PageComponent } from './components/page/page.component';
import { ListPagesComponent } from './components/list-pages/list-pages.component';

@NgModule({
  declarations: [
    AppComponent,
    DefaultLayoutComponent,
    LoginComponent,
    PageComponent,
    ListPagesComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ApiClientModule.forRoot()
  ],
  providers: [{
    provide: LocationStrategy,
    useClass: HashLocationStrategy
  },
    DefaultLayoutGlobals
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
