import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card/card.component';
import { HeaderComponent } from './card/header/header.component';
import { FooterComponent } from './card/footer/footer.component';

@NgModule({
  imports: [CommonModule],
  declarations: [CardComponent, HeaderComponent, FooterComponent],
  exports: [CardComponent, HeaderComponent, FooterComponent]
})
export class UiModule {}
