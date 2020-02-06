import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card.component';
import { HeaderComponent } from './header';
import { FooterComponent } from './footer';
import { BodyComponent } from './body';
import { ToolbarComponent } from './toolbar';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    CardComponent,
    HeaderComponent,
    FooterComponent,
    BodyComponent,
    ToolbarComponent
  ],
  exports: [
    CardComponent,
    HeaderComponent,
    FooterComponent,
    BodyComponent,
    ToolbarComponent
  ]
})
export class CardModule {}
