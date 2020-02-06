import { AccordionModule } from 'ngx-bootstrap/accordion';
import { ActionsService } from './actions.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PagerComponent } from './pager/pager.component';

@NgModule({
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    AccordionModule
  ],
  declarations: [
    PagerComponent
  ],
  exports: [
    PagerComponent,
    AccordionModule
  ],
  providers: [
    ActionsService
  ],
})
export class SharedModule { }
