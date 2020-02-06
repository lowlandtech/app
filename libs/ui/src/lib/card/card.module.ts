import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card.component';
import { HeaderComponent } from './header';
import { FooterComponent } from './footer';
import { BodyComponent } from './body';
import { ToolbarComponent } from './toolbar';
import { TitleComponent } from './title';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    CardComponent,
    HeaderComponent,
    FooterComponent,
    BodyComponent,
    ToolbarComponent,
    TitleComponent
  ],
  exports: [
    CardComponent,
    HeaderComponent,
    FooterComponent,
    BodyComponent,
    ToolbarComponent,
    TitleComponent
  ]
})
export class CardModule {}
