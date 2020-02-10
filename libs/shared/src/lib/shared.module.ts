import { AccordionModule } from 'ngx-bootstrap/accordion';
import { ActionsService } from './actions.service';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PagerComponent } from './pager/pager.component';
import { DynamicOverlay, DynamicOverlayContainer } from './services';
import { OverlayModule } from '@angular/cdk/overlay';

@NgModule({
  imports: [
    CommonModule,
    AccordionModule.forRoot(),
    OverlayModule
  ],
  declarations: [
    PagerComponent
  ],
  exports: [
    PagerComponent,
    AccordionModule,
    OverlayModule
  ],
  providers: [
    ActionsService,
    DynamicOverlay,
    DynamicOverlayContainer
  ]
})
export class SharedModule { }
