import { AccordionModule } from 'ngx-bootstrap/accordion';
import { ActionsService } from './actions.service';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PagerComponent } from './pager/pager.component';
import { DynamicOverlay, DynamicOverlayContainer } from './services';
import { OverlayModule } from '@angular/cdk/overlay';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  imports: [
    CommonModule,
    AccordionModule.forRoot(),
    OverlayModule,
    DragDropModule,
    CollapseModule.forRoot()
  ],
  declarations: [
    PagerComponent
  ],
  exports: [
    PagerComponent,
    AccordionModule,
    OverlayModule,
    DragDropModule,
    CollapseModule
  ],
  providers: [
    ActionsService,
    DynamicOverlay,
    DynamicOverlayContainer
  ]
})
export class SharedModule { }
