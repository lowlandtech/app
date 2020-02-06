import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card/card.component';
import { HeaderComponent } from './card/header/header.component';

@NgModule({
  imports: [CommonModule],
  declarations: [CardComponent, HeaderComponent],
  exports: [CardComponent, HeaderComponent]
})
export class UiModule {}
