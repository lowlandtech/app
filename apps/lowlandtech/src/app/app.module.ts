// imports external modules;
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { NxModule } from '@nrwl/angular';

import { environment } from '../environments/environment';

// imports library modules;
import { AdminModule } from '@lowlandtech/admin';
import { ApiModule } from '@lowlandtech/api';
import { AuthModule } from '@lowlandtech/auth';
import { NgrxErrorModule } from '@lowlandtech/ngrx-error';
import { NgrxRouterModule } from '@lowlandtech/ngrx-router';

// imports routing module;
import { AppRoutingModule } from './app.routing';

// imports components;
import { AppComponent } from './app.component';
import { AdminComponent } from './layouts';
import { SiteComponent } from './layouts';

@NgModule({
  imports: [
    ApiModule,
    AuthModule,
    BrowserModule,
    BrowserAnimationsModule,
    NxModule.forRoot(),
    AppRoutingModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    AdminModule,
    StoreModule.forRoot({}),
    EffectsModule.forRoot([]),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    NgrxRouterModule,
    NgrxErrorModule
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
