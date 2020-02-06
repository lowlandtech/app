import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card';
import { HeaderComponent } from './card';
import { FooterComponent } from './card';
import { BodyComponent } from './card';
import { ToolbarComponent } from './card';

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
export class UiModule {}
