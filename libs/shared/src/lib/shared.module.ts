import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagerComponent } from './pager/pager.component';
import { ActionsService } from './actions.service';
import { AccordionModule } from 'ngx-bootstrap/accordion';

@NgModule({
  imports: [CommonModule,AccordionModule],
  declarations: [PagerComponent],
  exports: [PagerComponent, AccordionModule],
  providers: [ActionsService],
})
export class SharedModule {}
