import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card/card.component';
import { HeaderComponent } from './card/header/header.component';
import { FooterComponent } from './card/footer/footer.component';
import { BodyComponent } from './card/body/body.component';

@NgModule({
  imports: [CommonModule],
  declarations: [CardComponent, HeaderComponent, FooterComponent, BodyComponent],
  exports: [CardComponent, HeaderComponent, FooterComponent, BodyComponent]
})
export class UiModule {}
