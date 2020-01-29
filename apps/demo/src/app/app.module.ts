import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';

import { AppComponent } from './app.component';

// Import containers
import {
  AdminComponent,
  SiteComponent
} from './layouts';

const APP_CONTAINERS = [
  AdminComponent,
  SiteComponent
]

// Import directives
import {
  NAV_DROPDOWN_DIRECTIVES,
  ReplaceDirective,
} from './directives';

const APP_DIRECTIVES = [
  NAV_DROPDOWN_DIRECTIVES,
  ReplaceDirective,
]

// Import routing module
import { AppRoutingModule } from './app.routing';


// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { AsideModule } from '@spotacard/aside';
import { FooterModule } from '@spotacard/footer';
import { BreadcrumbsModule } from '@spotacard/breadcrumbs';
import { HeaderModule } from '@spotacard/header';
import { ContentModule } from '@spotacard/content';
import { SidebarModule } from '@spotacard/sidebar';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    AsideModule,
    FooterModule,
    BreadcrumbsModule,
    HeaderModule,
    ContentModule,
    SidebarModule
  ],
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    ...APP_DIRECTIVES
  ],
  providers: [{
    provide: LocationStrategy,
    useClass: HashLocationStrategy
  }],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
