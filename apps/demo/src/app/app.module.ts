// imports external modules;
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';

// imports library modules;
import { AsideModule } from '@spotacard/aside';
import { FooterModule } from '@spotacard/footer';
import { BreadcrumbsModule } from '@spotacard/breadcrumbs';
import { HeaderModule } from '@spotacard/header';
import { ContentModule } from '@spotacard/content';
import { SidebarModule } from '@spotacard/sidebar';

// imports routing module;
import { AppRoutingModule } from './app.routing';

// imports components;
import { AppComponent } from './app.component';
import { AdminComponent } from './layouts';
import { SiteComponent } from './layouts';

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
    AdminComponent,
    SiteComponent
  ],
  providers: [{
    provide: LocationStrategy,
    useClass: HashLocationStrategy
  }],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
