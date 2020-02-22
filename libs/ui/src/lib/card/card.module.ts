import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@spotacard/shared';
import { CardComponent } from './card.component';
import { CloseableComponent } from './closeable';
@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [
    CardComponent,
    CloseableComponent
  ],
  exports: [
    CardComponent,
    CloseableComponent
  ],
  entryComponents:[
    CloseableComponent
  ]
})
export class CardModule {}
