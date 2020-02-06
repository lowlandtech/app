import { AccordionModule } from 'ngx-bootstrap/accordion';
import { ActionsService } from './actions.service';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PagerComponent } from './pager/pager.component';

@NgModule({
  imports: [
    CommonModule,
    AccordionModule.forRoot()
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
