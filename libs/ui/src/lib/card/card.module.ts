import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card.component';
import { CloseableComponent } from './closeable/closeable.component';

@NgModule({
  imports: [CommonModule],
  declarations: [
    CardComponent,
    CloseableComponent
  ],
  exports: [
    CardComponent,
    CloseableComponent
  ]
})
export class CardModule {}
