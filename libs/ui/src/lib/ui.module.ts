import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card/card.component';
import { HeaderComponent } from './card/header/header.component';
import { FooterComponent } from './card/footer/footer.component';
import { BodyComponent } from './card/body/body.component';
import { ToolbarComponent } from './card/toolbar/toolbar.component';

@NgModule({
  imports: [CommonModule],
  declarations: [CardComponent, HeaderComponent, FooterComponent, BodyComponent, ToolbarComponent],
  exports: [CardComponent, HeaderComponent, FooterComponent, BodyComponent, ToolbarComponent]
})
export class UiModule {}
